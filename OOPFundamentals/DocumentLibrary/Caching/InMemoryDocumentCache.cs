using DocumentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibrary.Caching;

public class InMemoryDocumentCache : IDocumentCache
{
    public void Add(Document document)
    {
        throw new NotImplementedException();
    }

    public bool TryGet(int id, out IEnumerable<Document> documents)
    {
        throw new NotImplementedException();
    }
}
