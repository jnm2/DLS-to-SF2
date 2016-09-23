using System;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsInfo
    {
        public DlsInfo(string archivalLocation, string artist, string commissioned, string comments, string copyright, DateTime? creationDate, string engineer, string genre, string keywords, string medium, string name, string product, string subject, string software, string source, string sourceForm, string technician)
        {
            ArchivalLocation = archivalLocation;
            Artist = artist;
            Commissioned = commissioned;
            Comments = comments;
            Copyright = copyright;
            CreationDate = creationDate;
            Engineer = engineer;
            Genre = genre;
            Keywords = keywords;
            Medium = medium;
            Name = name;
            Product = product;
            Subject = subject;
            Software = software;
            Source = source;
            SourceForm = sourceForm;
            Technician = technician;
        }

        public string ArchivalLocation { get; }
        public string Artist { get; }
        public string Commissioned { get; }
        public string Comments { get; }
        public string Copyright { get; }
        public DateTime? CreationDate { get; }
        public string Engineer { get; }
        public string Genre { get; }
        public string Keywords { get; }
        public string Medium { get; }
        public string Name { get; }
        public string Product { get; }
        public string Subject { get; }
        public string Software { get; }
        public string Source { get; }
        public string SourceForm { get; }
        public string Technician { get; }
    }
}