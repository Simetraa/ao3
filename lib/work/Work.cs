using AngleSharp;
using ao3.lib.author;

namespace ao3.lib.work
{
    public class Work(int id, string title, string description, string author, string language, int completedChapters, int? totalChapters, int words, int kudos, int bookmarks, int hits, bool completed, DateOnly? updated, DateOnly published, Rating rating, Warning archiveWarning, Category category, List<string> fandoms, List<string> relationships, List<string> characters, List<string> tags, string? text) : WorkBase(id, title, rating, archiveWarning, category, fandoms, relationships, characters, completed, description, author, tags, language, words, completedChapters, totalChapters, kudos, bookmarks, hits)
    {
        public DateOnly? Updated { get; } = updated;

        public DateOnly Published { get; } = published;

        public string? Text { get; } = text;

        public async Task<Author> GetAuthor()
        {
            return await Author.ParseAsync(AuthorString);
        }



        public static Work ParseFromWork(AngleSharp.Dom.IDocument document)
        {
            var idSelector = ".share a";
            var idString = document.QuerySelector(idSelector)!.GetAttribute("href")!;
            var id = Utils.ParseNumber(idString.Split("/")[2]);


            var titleSelector = "h2.title";

            var title = document.QuerySelector(titleSelector)!.TextContent;
            title = title.Trim();

            var summarySelector = ".summary p";
            var summary = document.QuerySelector(summarySelector)!.TextContent;

            var authorSelector = "h3.byline a";
            var author = document.QuerySelector(authorSelector)!.TextContent;

            var languageSelector = "dd.language";
            var language = document.QuerySelector(languageSelector)!.TextContent;
            language = language.Trim();


            var tagsSelector = ".freeform .tag";
            var tags = document.QuerySelectorAll(tagsSelector).Select(t => t.TextContent).ToList();


            var updatedSelector = "dd.updated";
            var updatedEl = document.QuerySelector(updatedSelector);
            var updatedString = updatedEl?.TextContent;
            DateOnly? updated = updatedString == null ? null : Utils.ParseDate(updatedString!);

            var publishedSelector = "dd.published";
            var publishedString = document.QuerySelector(publishedSelector)!.TextContent;
            var published = Utils.ParseDate(publishedString);

            var wordsSelector = "dd.words";
            var wordsString = document.QuerySelector(wordsSelector)!.TextContent;
            var words = Utils.ParseNumber(wordsString);

            var kudosSelector = "dd.kudos";
            var kudosString = document.QuerySelector(kudosSelector)!.TextContent;
            var kudos = Utils.ParseNumber(kudosString);

            var hitsSelector = "dd.hits";
            var hitsString = document.QuerySelector(hitsSelector)!.TextContent;
            var hits = Utils.ParseNumber(hitsString);

            var bookmarksSelector = "dd.hits";
            var bookmarksString = document.QuerySelector(bookmarksSelector)!.TextContent;
            var bookmarks = Utils.ParseNumber(bookmarksString);


            var chaptersSelector = "dd.chapters";
            var chapterString = document.QuerySelector(chaptersSelector)!.TextContent;
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);


            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);

            var ratingSelector = ".rating .tag";
            var ratingString = document.QuerySelector(ratingSelector)!.TextContent;
            var rating = Utils.RatingDict[ratingString];

            var archiveWarningSelector = ".warning .tag";
            var archiveWarningString = document.QuerySelector(archiveWarningSelector)!.TextContent;
            var archiveWarning = Utils.WarningDict[archiveWarningString];

            var categorySelector = ".category .tag";
            var categoryString = document.QuerySelector(categorySelector)!.TextContent;
            var category = Utils.CategoryDict[categoryString];

            var fandomsSelector = ".fandom .tag";
            var fandoms = document.QuerySelectorAll(fandomsSelector).Select(t => t.TextContent).ToList();

            var relationshipsSelector = ".relationship .tag";
            var relationships = document.QuerySelectorAll(relationshipsSelector).Select(t => t.TextContent).ToList();


            var charactersSelector = ".character .tag";
            var characters = document.QuerySelectorAll(charactersSelector).Select(t => t.TextContent).ToList();

            var completed = totalChapters == chapters; // TODO: Is this always correct? There is no completed tag on full fics.

            var textSelector = "#chapters";
            var text = document.QuerySelector(textSelector)!.TextContent;



            return new Work(id, title, summary, author, language, chapters, totalChapters, words, kudos, bookmarks, hits,
                            completed, updated, published, rating, archiveWarning, category, fandoms, relationships,
                            characters, tags, text);
        }





        public static async Task<Work> ParseFromIdAsync(int id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"https://archiveofourown.org/works/{id}/?view_full_work=true";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            return ParseFromWork(document);
        }
    }

}
