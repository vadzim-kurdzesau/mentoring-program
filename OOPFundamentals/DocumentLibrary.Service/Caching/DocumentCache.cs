using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using DocumentLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DocumentLibrary.Service.Caching
{
    public class DocumentCache : IDocumentCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<Type, MemoryCacheEntryOptions> _options = new();

        public DocumentCache()
            : this(ImmutableDictionary<Type, DocumentCachingOptions>.Empty)
        {
        }

        public DocumentCache(IDictionary<Type, DocumentCachingOptions> options)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            foreach (var (type, option) in options)
            {
                if (!option.ShouldBeCached)
                {
                    continue;
                }

                var entryOptions = new MemoryCacheEntryOptions();
                if (option.ExpirationTime.Value != Timeout.InfiniteTimeSpan)
                {
                    entryOptions.SlidingExpiration = option.ExpirationTime;
                }

                _options.Add(type, entryOptions);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="document"/> is null.</exception>
        public void Add(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            // Do not cache not configured document types
            if (!_options.TryGetValue(document.GetType(), out var options))
            {
                return;
            }

            var cacheKey = new MemoryCacheDocumentKey(document);
            if (_memoryCache.TryGetValue(cacheKey, out var _))
            {
                _memoryCache.Remove(cacheKey);
            }

            _memoryCache.Set(cacheKey, document, options);
        }

        public bool TryGet(Type type, int id, out Document document)
        {
            var cacheKey = new MemoryCacheDocumentKey(id, type);
            return _memoryCache.TryGetValue(cacheKey, out document);
        }

        /// <summary>
        /// Encapsulates both document type and document ID in one key.
        /// </summary>
        private class MemoryCacheDocumentKey
        {
            public MemoryCacheDocumentKey(Document document)
                : this(document.DocumentId, document.GetType())
            {
            }

            public MemoryCacheDocumentKey(int id, Type type)
            {
                Id = id;
                Type = type;
            }

            public int Id { get; set; }

            public Type Type { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is MemoryCacheDocumentKey otherKey)
                {
                    return otherKey.Id == Id && otherKey.Type.Name.Equals(Type.Name, StringComparison.InvariantCulture);
                }

                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                // Quick id comparison to speed up comparison of documents with different id's
                return Id;
            }
        }
    }
}
