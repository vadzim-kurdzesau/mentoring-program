namespace DocumentLibrary.Models;

public class Book : Document
{
    public string ISBN { get; set; }

    public string[] Authors { get; set; }

    public int NumberOfPages { get; set; }

    public string Publisher { get; set; }
}
