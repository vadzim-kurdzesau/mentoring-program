using DocumentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibrary;

public class CachingDocumentRepository : IDocumentRepository
{
    private readonly IDocumentRepository _repository;

    public CachingDocumentRepository(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public void Add(Document document)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Document> Get(int documentNumber)
    {
        throw new NotImplementedException();
    }
}
