using System.Collections.Generic;
using DocumentLibrary.Models;

namespace DocumentLibrary.Caching;

/// <summary>
/// Caches documents.
/// </summary>
public interface IDocumentCache
{
    /// <summary>
    /// Tries to get documents with specified <paramref name="id"/>.
    /// </summary>
    public bool TryGet(int id, out IEnumerable<Document> documents);

    /// <summary>
    /// Adds <paramref name="document"/> to cache.
    /// </summary>
    public void Add(Document document);
}
