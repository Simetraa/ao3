using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ao3
{
    internal class Work
    {
        public Work() { }

        string Title { get; }
        Author Author { get; }
        public DateOnly Published { get; }
        public DateOnly Updated {  get; }
        List<string> Tags { get; }
        string Language { get; }
        int Words { get; }
        int Chapters { get; }
        int? TotalChapters { get; }
        int Kudos { get; }
        int Bookmarks { get; } // eventually create classes for Kudos & Bookmarks
        int Hits { get; }
    
        static async Task<Work> ParseAsync(int id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/works/50865955/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var titleSelector = "h2.title";
            var title = document.QuerySelector(titleSelector).TextContent;

            var publishedSelector = "dd.published";
            var publishedString = document.QuerySelector(publishedSelector).TextContent;
            var published = Utils.ParseDate(publishedString);


            var updatedSelector = "dd.status";
            var updatedString = document.QuerySelector(updatedSelector).TextContent;
            var updated = Utils.ParseDate(updatedString);

            var wordsSelector = "dd.words";
            var words = document.QuerySelector(updatedSelector).TextContent;

            var userIdSelector = "dl[class='meta'] > dd:nth-child(6)";
            var userIdString = document.QuerySelector(userIdSelector).TextContent;


            return new Work();
        }
    }

}
