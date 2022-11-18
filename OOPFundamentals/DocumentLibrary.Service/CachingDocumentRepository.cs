using System;
using System.Collections.Generic;
using DocumentLibrary.Models;
using DocumentLibrary.Service.Caching;

namespace DocumentLibrary.Service
{
    public class CachingDocumentRepository : IDocumentRepository
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentCache _documentCache;

        public CachingDocumentRepository(IDocumentRepository documentRepository, IDocumentCache documentCache)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _documentCache = documentCache ?? throw new ArgumentNullException(nameof(documentCache));
        }

        public void Add(Document document)
        {
            _documentCache.Add(document);
            _documentRepository.Add(document);
        }

        public IEnumerable<Document> Get(int documentNumber)
        {
            if (_documentCache.TryGet(documentNumber, out var document))
            {
                //return document;
            }

            return _documentRepository.Get(documentNumber);
        }
    }
}
