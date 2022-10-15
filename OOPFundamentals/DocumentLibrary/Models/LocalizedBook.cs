namespace DocumentLibrary.Models;

public class LocalizedBook : Book
{
    public string LocalizationCountry { get; set; }

    public string LocalPublisher { get; set; }
}
