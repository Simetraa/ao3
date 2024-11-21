using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ao3
{
    class WorkMeta
    {
        public int Id { get; }
        public string Title { get; }
        public Rating Rating { get; }
        public Warning ArchiveWarning { get; }
        public Category Category { get; }
        public List<string> Fandoms { get; }
        public List<string> Relationships { get; }
        public List<string> Characters { get; }
        public bool Completed { get; }
        public string Description { get; }
        // can there be multiple authors on a fic?
        public string AuthorString { get; }
        public DateOnly? Published { get; }
        public DateOnly? Updated { get; }
        public List<string> FreeformTags { get; }
        public string Language { get; }
        public int Words { get; }
        public int CompletedChapters { get; }
        public int? TotalChapters { get; }
        public int Kudos { get; }
        public int? Bookmarks { get; } // eventually create classes for Kudos & Bookmarks
        public int Hits { get; }
    }
}
