using DocumentLibrary.Models;
using DocumentLibrary.Service;
using DocumentLibrary.Service.Caching;

namespace DocumentLibrary.Demo;

internal class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("Specify the path to the storage directory.");
        }

        var directoryPath = args[0];

        var cacheConfiguration = new Dictionary<Type, DocumentCachingOptions>
        {
            { typeof(Patent), new() { ExpirationTime = Timeout.InfiniteTimeSpan } },
            { typeof(Book), new() { ExpirationTime = TimeSpan.FromSeconds(10) } },
            { typeof(LocalizedBook), new() },
        };

        var repository = new CachingDocumentRepository(
            new JsonDocumentRepository(directoryPath), new DocumentCache(cacheConfiguration));

        while (true)
        {
            var documentType = ReadDocumentType();

            Console.Write("Specify the document number: ");
            if (!int.TryParse(Console.ReadLine(), out int documentNumber))
            {
                throw new ArgumentException("Specify the integer document number.");
            }

            var document = repository.Get(documentType, documentNumber);
            if (document != null)
            {
                Console.WriteLine(document.GetInfo());
            }
        }
    }

    private static Type ReadDocumentType()
    {
        Console.WriteLine("1) Patent            2) Book");
        Console.WriteLine("3) Localized Book    4) Magazine");
        Console.Write("Select a document type: ");

        if (!int.TryParse(Console.ReadLine(), out int documentType))
        {
            throw new ArgumentException("Specify the integer number of document type.");
        }

        return documentType switch
        {
            1 => typeof(Patent),
            2 => typeof(Book),
            3 => typeof(LocalizedBook),
            4 => typeof(Magazine),
            _ => throw new ArgumentException()
        };
    }
}
