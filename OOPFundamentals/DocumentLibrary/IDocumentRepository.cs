using System;
using DocumentLibrary.Models;

namespace DocumentLibrary
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Adds the <paramref name="document"/> to storage.
        /// </summary>
        public void Add(Document document);

        /// <summary>
        /// Gets the <see cref="Document"/> with specified <paramref name="documentNumber"/>.
        /// </summary>
        /// <returns><see cref="Document"/> if exists; otherwise null.</returns>
        public Document? Get(Type type, int documentNumber);
    }
}
