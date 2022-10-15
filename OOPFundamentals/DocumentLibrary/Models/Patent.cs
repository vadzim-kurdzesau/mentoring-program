using System;

namespace DocumentLibrary.Models;

public class Patent : Document
{
    public string UniqueID { get; set; }

    public string[] Authors { get; set; }

    public DateTime ExpirationDate { get; set; }
}
