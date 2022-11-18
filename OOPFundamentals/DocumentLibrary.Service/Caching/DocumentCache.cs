using System;
using System.Collections.Generic;
using DocumentLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DocumentLibrary.Service.Caching;

public class DocumentCache : IDocumentCache
{
    private static readonly MemoryCacheEntryOptions DefaultOptions = new()
    {
        SlidingExpiration = new TimeSpan(0, 0, 10)
    };

    private readonly IMemoryCache _memoryCache;
    private readonly Dictionary<Type, MemoryCacheEntryOptions> _options = new();

    public DocumentCache(Dictionary<Type, MemoryCacheEntryOptions> options, int maxSize = 1024)
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions { SizeLimit = maxSize });
        foreach (var (type, option) in options)
        {
            _options.Add(type, option);
        }
    }

    public void Add(Document document)
    {
        if (!_options.TryGetValue(document.GetType(), out var options))
        {
            options = DefaultOptions;
        }

        if (_memoryCache.TryGetValue(document.DocumentId, out var _))
        {
            _memoryCache.Remove(document.DocumentId);
        }

        _memoryCache.Set(document.DocumentId, document, options);
    }

    public bool TryGet(int id, out Document? document)
    {
        return _memoryCache.TryGetValue(id, out document);
    }
}
