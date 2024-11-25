using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ao3.lib
{
    public class WorkMeta(int id, string title, Rating rating, Warning archiveWarning, Category category, List<string> fandoms, List<string> relationships, List<string> characters, bool completed, string description, string authorString, List<string> freeformTags, string language, int words, int completedChapters, int? totalChapters, int kudos, int bookmarks, int hits)
    {
        public int Id { get; } = id;
        public string Title { get; } = title;
        public Rating Rating { get; } = rating;
        public Warning ArchiveWarning { get; } = archiveWarning;
        public Category Category { get; } = category;
        public List<string> Fandoms { get; } = fandoms;
        public List<string> Relationships { get; } = relationships;
        public List<string> Characters { get; } = characters;
        public bool Completed { get; } = completed;
        public string Description { get; } = description;
        // can there be multiple authors on a fic?
        public string AuthorString { get; } = authorString;
        public List<string> FreeformTags { get; } = freeformTags;
        public string Language { get; } = language;
        public int Words { get; } = words;
        public int CompletedChapters { get; } = completedChapters;
        public int? TotalChapters { get; } = totalChapters;
        public int Kudos { get; } = kudos;
        public int Bookmarks { get; } = bookmarks;
        public int Hits { get; } = hits;

        public static WorkMeta ParseFromMeta(IElement html)
        {
            var idSelector = ".header .heading:first-child a:first-child";
            var idString = html.QuerySelector(idSelector)!.GetAttribute("href")!.Split("/").Last();
            var id = Utils.ParseNumber(idString);

            var titleSelector = ".header .heading:first-child a:first-child";
            var title = html.QuerySelector(titleSelector)!.TextContent;

            var summarySelector = ".summary p";
            var summary = html.QuerySelector(summarySelector)!.TextContent;

            var authorSelector = ".header .heading:first-child a[rel='author']";
            var author = html.QuerySelector(authorSelector)!.TextContent;

            var languageSelector = "dd.language";
            var language = html.QuerySelector(languageSelector)!.TextContent;


            var updatedSelector = ".datetime";
            var updatedString = html.QuerySelector(updatedSelector)!.TextContent;
            var updated = Utils.ParseMetaDate(updatedString);

            var wordsSelector = "dd.words";
            var wordsString = html.QuerySelector(wordsSelector)!.TextContent;
            var words = Utils.ParseNumber(wordsString);


            var hitsSelector = "dd.hits";
            var hitsString = html.QuerySelector(hitsSelector)!.TextContent;
            var hits = Utils.ParseNumber(hitsString);


            var chaptersSelector = "dd.chapters";
            var chapterString = html.QuerySelector(chaptersSelector)!.TextContent;
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);


            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);

            var kudosSelector = "dd.kudos";
            var kudosString = html.QuerySelector(kudosSelector)!.TextContent;
            var kudos = Utils.ParseNumber(kudosString);

            var bookmarksSelector = "dd.kudos";
            var bookmarksString = html.QuerySelector(bookmarksSelector)!.TextContent;
            var bookmarks = Utils.ParseNumber(bookmarksString);

            var ratingSelector = ".required-tags .rating span";
            var ratingsString = html.QuerySelector(ratingSelector)!.TextContent;
            var rating = Utils.RatingDict[ratingsString];

            var archiveWarningSelector = ".required-tags .warnings span";
            var archiveWarningString = html.QuerySelector(archiveWarningSelector)!.TextContent;
            var archiveWarning = Utils.WarningDict[archiveWarningString];

            var categorySelector = ".required-tags .category span";
            var categoryString = html.QuerySelector(categorySelector)!.TextContent;
            var category = Utils.CategoryDict[categoryString];

            var fandomsSelector = ".fandoms .tag";
            var fandoms = html.QuerySelectorAll(fandomsSelector).Select(t => t.TextContent).ToList();

            var relationshipsSelector = ".relationships .tag";
            var relationships = html.QuerySelectorAll(relationshipsSelector).Select(t => t.TextContent).ToList();

            var charactersSelector = ".characters .tag";
            var characters = html.QuerySelectorAll(charactersSelector).Select(t => t.TextContent).ToList();

            var freeformTagsSelector = ".freeforms .tag";
            var freeformTags = html.QuerySelectorAll(freeformTagsSelector).Select(t => t.TextContent).ToList();

            var completedSelector = ".required-tags .iswip span";
            var completedString = html.QuerySelector(completedSelector)!.TextContent;
            var completed = completedString == "Complete Work";

            return new WorkMeta(id, title, rating, archiveWarning, category, fandoms, relationships, characters, completed, summary, author, freeformTags, language, words, chapters, totalChapters, kudos, bookmarks, hits);
        }
        public async Task<Work> ToWork()
        {
            return await Work.ParseFromIdAsync(Id);
        }
    }
}
