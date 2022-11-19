namespace DocumentLibrary.Models
{
    public class Book : Document
    {
        public string ISBN { get; set; }

        public string[] Authors { get; set; }

        public int NumberOfPages { get; set; }

        public string Publisher { get; set; }

        public override string GetInfo()
        {
            return $"{DocumentId}, Title: '{Title}' by {string.Join(',', Authors)}," +
                $" {PublicationDate.ToString("dd MMMM yyyy")}, ISBN: {ISBN}, Pages: {NumberOfPages}, Publisher: {Publisher}";
        }
    }
}
