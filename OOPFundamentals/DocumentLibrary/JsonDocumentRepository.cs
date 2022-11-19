using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Document = DocumentLibrary.Models.Document;

namespace DocumentLibrary
{
    public class JsonDocumentRepository : IDocumentRepository
    {
        private const string filePattern = "{0}_#{1}.json";
        private static readonly JsonSerializerSettings serializerSettings = new() { TypeNameHandling = TypeNameHandling.Objects };
        private readonly string _directoryPath;

        public JsonDocumentRepository(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException($"Directory '{directoryPath}' does not exist.");
            }

            _directoryPath = directoryPath;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="document"/> is null.</exception>
        public void Add(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var filePath = GetPathToDocument(document);
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                var serializedDocument = JsonConvert.SerializeObject(document, serializerSettings);
                using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.Write(serializedDocument);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="type"/> is null.</exception>
        public Document? Get(Type type, int documentNumber)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var filePath = GetPathToDocument(type, documentNumber);
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    var serializedDocument = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<Document>(serializedDocument, serializerSettings);
                }
            }
        }

        private string GetPathToDocument(Document document) => GetPathToDocument(document.GetType(), document.DocumentId);

        private string GetPathToDocument(Type type, int documentId)
        {
            return Path.Combine(_directoryPath, string.Format(filePattern, type.Name, documentId));
        }
    }
}
