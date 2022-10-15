using DocumentLibrary.Models;
using System.Collections.Generic;

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
        public IEnumerable<Document> Get(int documentNumber);
    }
}
