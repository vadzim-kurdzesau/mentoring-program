using System;

namespace DocumentLibrary.Models
{
    public abstract class Document
    {
        public int DocumentId { get; set; }

        public string? Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public abstract string GetInfo();
    }
}
