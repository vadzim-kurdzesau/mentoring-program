using DocumentLibrary;
using DocumentLibrary.Models;

var repository = new JsonDocumentRepository(@"C:\Users\Vadzim_Kurdzesau\source\repos\Test");

var book = new Book()
{
    DocumentId = 1,
    Title = "TestBook1",
    ISBN = "123456789",
    NumberOfPages = 100,
    PublicationDate = DateTime.Now,
    Publisher = "TestPublisher1",
    Authors = new[] { "Vadzim" }
};

var localizedBook = new LocalizedBook()
{
    DocumentId = 1,
    Title = "TestLocalizedBook1",
    ISBN = "123456789_B",
    NumberOfPages = 105,
    PublicationDate = DateTime.Now,
    Publisher = "TestPublisher1",
    Authors = new[] { "Vadzim" },
    LocalizationCountry = "Belarus",
    LocalPublisher = "TestLocalizedPublisher1"
};

repository.Add(book);
repository.Add(localizedBook);

var books = repository.Get(book.DocumentId).ToList();

var a = 5;