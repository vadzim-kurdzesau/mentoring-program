using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Document = DocumentLibrary.Models.Document;

namespace DocumentLibrary
{
    public class JsonDocumentRepository : IDocumentRepository
    {
        private const string filePattern = "*_#{0}.json";
        private static readonly JsonSerializerSettings serializerSettings = new() { TypeNameHandling = TypeNameHandling.Objects };
        private readonly string _directoryPath;

        public JsonDocumentRepository(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Add(Document document)
        {
            var filePath = GetPathToDocument(document);
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                SerializeToStream(fileStream, document);
            }
        }

        public IEnumerable<Document> Get(int documentNumber)
        {
            var filePaths = Directory.GetFiles(_directoryPath, string.Format(filePattern, documentNumber));
            foreach (var filePath in filePaths)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        var serializedDocument = streamReader.ReadToEnd();
                        yield return JsonConvert.DeserializeObject<Document>(serializedDocument, serializerSettings);
                    }
                }
            }
        }

        private string GetPathToDocument(Document document)
        {
            return Path.Combine(_directoryPath, $"{document.GetType().Name}_#{document.DocumentId}.json");
        }

        private static void SerializeToStream(Stream stream, Document document)
        {
            var serializedDocument = JsonConvert.SerializeObject(document, serializerSettings);
            using (var streamWriter = new StreamWriter(stream, Encoding.UTF8))
            {
                streamWriter.Write(serializedDocument);
            }
        }
    }
}
