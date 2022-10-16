using System;

namespace DocumentLibrary.Models;

public class Patent : Document
{
    private const string DateFormat = "dd MMMM yyyy";

    public string UniqueID { get; set; }

    public string[] Authors { get; set; }

    public DateTime ExpirationDate { get; set; }

    public override string GetInfo()
    {
        return $"{DocumentId}, UID: {UniqueID}, Title: '{Title}' by {string.Join(',', Authors)}," +
                $" {PublicationDate.ToString(DateFormat)}, Expires: {ExpirationDate.ToString(DateFormat)}";
    }
}
