using System;
using DocumentLibrary.Models;
using DocumentLibrary.Service.Caching;
using Microsoft.Extensions.Logging;

namespace DocumentLibrary.Service
{
    public class CachingDocumentRepository : IDocumentRepository
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentCache _documentCache;
        private readonly ILogger _logger;

        public CachingDocumentRepository(
            IDocumentRepository documentRepository, IDocumentCache documentCache, ILogger<CachingDocumentRepository> logger = null)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentCache = documentCache ?? throw new ArgumentNullException(nameof(documentCache));
            _logger = logger;
        }

        public void Add(Document document)
        {
            _documentCache.Add(document);
            _logger?.LogDebug("Cached document of '{Type}' type with '{Id}' ID.", document.GetType().Name, document.DocumentId);
            _documentRepository.Add(document);
        }

        public Document Get(Type type, int documentNumber)
        {
            if (_documentCache.TryGet(type, documentNumber, out var document))
            {
                _logger?.LogDebug("Retrieved document of '{Type}' type with '{Id}' ID from cache.", type.Name, documentNumber);
                return document;
            }

            document = _documentRepository.Get(type, documentNumber);
            if (document != null)
            {
                _documentCache.Add(document);
                _logger?.LogDebug("Cached retrieved from repository document of '{Type}' type with '{Id}' ID.", type.Name, documentNumber);
            }

            return document;
        }
    }
}
