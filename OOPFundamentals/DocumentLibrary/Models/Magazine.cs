namespace DocumentLibrary.Models
{
    public class Magazine : Document
    {
        public string Publisher { get; set; }

        public string ReleaseNumber { get; set; }

        public override string GetInfo()
        {
            return $"{DocumentId}, Title: '{Title}', {PublicationDate.ToString("dd MMMM yyyy")}, Release Number: {ReleaseNumber}, Publisher: {Publisher}";
        }
    }
}
