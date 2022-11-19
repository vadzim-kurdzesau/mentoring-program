using System;
using DocumentLibrary.Models;

namespace DocumentLibrary.Service.Caching
{
    public interface IDocumentCache
    {
        /// <summary>
        /// Adds the <paramref name="document"/> to cache.
        /// </summary>
        void Add(Document document);

        /// <summary>
        /// Tries to get the <see cref="Document"/> with specified <paramref name="id"/> from cache.
        /// </summary>
        /// <returns>Document with the specified <paramref name="id"/> or null, if doesn't not exist in cache.</returns>
        bool TryGet(Type type, int id, out Document document);
    }
}
