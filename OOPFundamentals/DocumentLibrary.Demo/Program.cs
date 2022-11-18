using DocumentLibrary;
using DocumentLibrary.Models;

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
        var repository = new JsonDocumentRepository(directoryPath);

        while (true)
        {
            Console.Write("Specify the document number: ");
            if (!int.TryParse(Console.ReadLine(), out int documentNumber))
            {
                throw new ArgumentException("Specify the integer document number.");
            }

            var documents = repository.Get(documentNumber);
            foreach (var document in documents)
            {
                Console.WriteLine(document.GetInfo());
            }
        }
    }
}
