namespace DocumentLibrary.Models
{
    public class LocalizedBook : Book
    {
        public string LocalizationCountry { get; set; }

        public string LocalPublisher { get; set; }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Local Publisher: '{LocalPublisher}', Country of Localization: {LocalizationCountry}";
        }
    }
}
