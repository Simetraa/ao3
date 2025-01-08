using AngleSharp.Dom;

namespace ao3.lib.work
{

    public class WorkMeta(int id, string title, string description, string author, string language, int completedChapters, int? totalChapters, DateOnly updated, int words, int kudos, int bookmarks, int hits, bool completed, Rating rating, IEnumerable<Warning> archiveWarnings, IEnumerable<Category> categories, List<string> fandoms, List<string> relationships, List<string> characters, List<string> tags) : WorkBase(id, title, rating, archiveWarnings, categories, fandoms, relationships, characters, completed, description, author, tags, language, words, completedChapters, totalChapters, kudos, bookmarks, hits)
    {
        public DateOnly Updated { get; } = updated;


        // test on meta that doesnt have kudoes / bookmarks etc.
        // test on anonymous fic


        public static WorkMeta ParseFromMeta(IElement html)
        {
            var idSelector = ".header .heading:first-child a:first-child";
            var idString = html.QuerySelector(idSelector)!.GetAttribute("href")!.Split("/").Last();
            var id = Utils.ParseNumber(idString);

            var titleSelector = ".header .heading:first-child a:first-child";
            var title = html.QuerySelector(titleSelector)!.TextContent;

            var summarySelector = ".summary p";
            var summaryEl = html.QuerySelector(summarySelector);
            var summary = summaryEl?.TextContent ?? "";

            var authorSelector = ".header .heading:first-child a[rel='author']";
            var authorEl = html.QuerySelector(authorSelector);
            var author = authorEl?.TextContent ?? "Anonymous";

            var languageSelector = "dd.language";
            var language = html.QuerySelector(languageSelector)!.TextContent;


            var updatedSelector = ".datetime";
            var updatedString = html.QuerySelector(updatedSelector)!.TextContent;
            var updated = Utils.ParseMetaDate(updatedString);

            var wordsSelector = "dd.words";
            var wordsString = html.QuerySelector(wordsSelector)!.TextContent;
            var words = Utils.ParseNumber(wordsString);


            var hitsSelector = "dd.hits";
            var hitsString = html.QuerySelector(hitsSelector)!.TextContent ?? "0";
            var hits = Utils.ParseNumber(hitsString);


            var chaptersSelector = "dd.chapters";
            var chapterString = html.QuerySelector(chaptersSelector)!.TextContent ?? "0";
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);


            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);

            var kudosSelector = "dd.kudos";
            var kudosString = html.QuerySelector(kudosSelector)!?.TextContent ?? "0";
            var kudos = Utils.ParseNumber(kudosString);

            var bookmarksSelector = "dd.bookmarks";
            var bookmarksEl = html.QuerySelector(bookmarksSelector);
            var bookmarksString = bookmarksEl?.TextContent ?? "0";
            var bookmarks = Utils.ParseNumber(bookmarksString);

            var ratingSelector = ".required-tags .rating span";
            var ratingsString = html.QuerySelector(ratingSelector)!.TextContent;
            var rating = Utils.RatingDict[ratingsString];

            var archiveWarningsSelector = ".required-tags .warnings span";
            var archiveWarningsString = html.QuerySelector(archiveWarningsSelector)!.TextContent;
            var archiveWarnings = archiveWarningsString.Split(", ")
                .Where(Utils.WarningDict.ContainsKey)
                .Select(t => Utils.WarningDict[t])
                .ToList();


            var categoriesSelector = ".required-tags .category span";
            var categoriesString = html.QuerySelector(categoriesSelector)!.TextContent;
            var categories = categoriesString.Split(", ").Select(t => Utils.CategoryDict[t]).ToList();  

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


            return new WorkMeta(id,
                                title,
                                summary,
                                author,
                                language,
                                chapters,
                                totalChapters,
                                updated,
                                words,
                                kudos,
                                bookmarks,
                                hits,
                                completed,
                                rating,
                                archiveWarnings,
                                categories,
                                fandoms,
                                relationships,
                                characters,
                                freeformTags);
        }
        public async Task<Work> ToWork()
        {
            return await Work.ParseFromIdAsync(Id);
        }
    }
}
