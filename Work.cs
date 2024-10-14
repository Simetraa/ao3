using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AngleSharp;
using AngleSharp.Dom;

namespace ao3
{
    class Work
    {
        public Work(int id, string title, string description, string author, DateOnly published, List<string> tags, string language, int words, int chapters, int? totalChapters, int kudos, int bookmarks, int hits, DateOnly? updated = null)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorString = author;
            Published = published;
            Updated = updated;
            Tags = tags;
            Language = language;
            Words = words;
            Chapters = chapters;
            TotalChapters = totalChapters;
            Kudos = kudos;
            Bookmarks = bookmarks;
            Hits = hits;
        }

        int Id { get;  }
        string Title { get; }

        string Description { get;  }
        string AuthorString { get; }
        public DateOnly Published { get; }
        public DateOnly? Updated {  get; }
        List<string> Tags { get; }
        string Language { get; }
        int Words { get; }
        int Chapters { get; }
        int? TotalChapters { get; }
        int Kudos { get; }
        int Bookmarks { get; } // eventually create classes for Kudos & Bookmarks
        int Hits { get; }


        public async Task<Author> GetAuthor()
        {
            return await Author.ParseAsync(AuthorString);
        }

        public static Work ParseFromMeta(AngleSharp.Dom.IElement html)
        {
            var idSelector =".header .heading:first-child a:first-child";
            var idString = html.QuerySelector(idSelector).GetAttribute("href").Split("/").Last();
            var id = Utils.ParseNumber(idString);

            var titleSelector = ".header .heading:first-child a:first-child";
            var title = html.QuerySelector(titleSelector).TextContent;

            var summarySelector = ".summary p";
            var summary = html.QuerySelector(summarySelector).TextContent;

            var authorSelector = ".header .heading:first-child a[rel='author']";
            var author = html.QuerySelector(authorSelector).TextContent;

            var languageSelector = "dd.language";
            var language = html.QuerySelector(languageSelector).TextContent;

            var tagsSelector = ".tags .tag";
            var tags = html.QuerySelectorAll(tagsSelector).Select(t => t.TextContent).ToList();

            var publishedSelector = ".datetime";
            var publishedString = html.QuerySelector(publishedSelector).TextContent;
            var published = Utils.ParseMetaDate(publishedString);

            var wordsSelector = "dd.words";
            var wordsString = html.QuerySelector(wordsSelector).TextContent;
            var words = Utils.ParseNumber(wordsString);


            var hitsSelector = "dd.hits";
            var hitsString= html.QuerySelector(wordsSelector).TextContent;
            var hits = Utils.ParseNumber(wordsString);


            var chaptersSelector = "dd.chapters";
            var chapterString = html.QuerySelector(chaptersSelector).TextContent;
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);


            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);


            return new Work(id, title, summary, author, published, tags, language, words, chapters, totalChapters, 0, 0, hits);

        }

        public static async Task<Work> ParseFromIdAsync(int id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/works/50865955/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var titleSelector = "h2.title";
            var title = document.QuerySelector(titleSelector).TextContent;

            var summarySelector = ".summary p";
            var summary = document.QuerySelector(summarySelector).TextContent;

            var authorSelector = "h3.byline a";
            var author = document.QuerySelector(authorSelector).TextContent;

            var languageSelector = "dd.language";
            var language = document.QuerySelector(languageSelector).TextContent;


            var tagsSelector = ".meta .tag";
            var tags = document.QuerySelectorAll(tagsSelector).Select(t => t.TextContent).ToList();



            var publishedSelector = "dd.published";
            var publishedString = document.QuerySelector(publishedSelector).TextContent;
            var published = Utils.ParseDate(publishedString);


            var updatedSelector = "dd.status";
            var updatedString = document.QuerySelector(updatedSelector).TextContent;
            var updated = Utils.ParseDate(updatedString);

            var wordsSelector = "dd.words";
            var wordsString = document.QuerySelector(wordsSelector).TextContent;
            var words = Utils.ParseNumber(wordsString);


            var hitsSelector = "dd.hits";
            var hitsString = document.QuerySelector(hitsSelector).TextContent;
            var hits = Utils.ParseNumber(wordsString);


            var chaptersSelector = "dd.chapters";
            var chapterString = document.QuerySelector(chaptersSelector).TextContent;
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);


            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);



            return new Work(id, title, summary, author, published, new List<string>{ "" }, language, words, chapters, totalChapters, 0, 0, hits);

        }
    }

}
