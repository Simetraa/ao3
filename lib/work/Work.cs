﻿using AngleSharp;
using ao3.lib.author;
using ao3.lib.exceptions;

namespace ao3.lib.work
{
    public class Work(int id, string title, string description, string author, string language, int completedChapters,
                      int? totalChapters, int words, int kudos, int bookmarks, int hits, bool completed,
                      DateOnly? updated, DateOnly published, Rating rating, IEnumerable<Warning> archiveWarnings,
                      IEnumerable<Category> categories, IEnumerable<string> fandoms, IEnumerable<string> relationships,
                      IEnumerable<string> characters, IEnumerable<string> tags, string? text) : WorkBase(id, title, rating, archiveWarnings, categories, fandoms, relationships, characters, completed, description, author, tags, language, words, completedChapters, totalChapters, kudos, bookmarks, hits)
    {
        public DateOnly? Updated { get; private set; } = updated;
        public DateOnly Published { get; private set; } = published;
        public string? Text { get; private set; } = text;

        public async Task<Author> GetAuthor()
        {
            return await Author.ParseFromName(AuthorString);
        }

        public static Work ParseFromWork(AngleSharp.Dom.IDocument document)
        {
            if (document.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new WorkNotFoundException();
            }

            var idSelector = ".download ul li a";
            var idEl = document.QuerySelector(idSelector)!;
            var idString = idEl.GetAttribute("href")!;
            var id = Utils.ParseNumber(idString.Split("/")[2]);


            var titleSelector = "h2.title";

            var title = document.QuerySelector(titleSelector)!.TextContent;
            title = title.Trim();

            var summarySelector = ".summary p";
            var summaryEl = document.QuerySelector(summarySelector);
            var summary = summaryEl?.TextContent ?? "";


            var authorSelector = "h3.byline a";
            var authorEl = document.QuerySelector(authorSelector);
            var author = authorEl?.TextContent ?? "Anonymous";

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
            var kudosString = document.QuerySelector(kudosSelector)!?.TextContent ?? "0";
            var kudos = Utils.ParseNumber(kudosString);

            var hitsSelector = "dd.hits";
            var hitsString = document.QuerySelector(hitsSelector)!.TextContent ?? "0";
            var hits = Utils.ParseNumber(hitsString);

            var bookmarksSelector = "dd.bookmarks";
            var bookmarksEl = document.QuerySelector(bookmarksSelector);
            var bookmarksString = bookmarksEl?.TextContent ?? "0";
            var bookmarks = Utils.ParseNumber(bookmarksString);


            var chaptersSelector = "dd.chapters";
            var chapterString = document.QuerySelector(chaptersSelector)!.TextContent ?? "0";
            var chapters = Utils.ParseNumber(chapterString.Split("/")[0]);



            var totalChaptersString = chapterString.Split("/")[1];
            int? totalChapters = totalChaptersString == "?" ? null : Utils.ParseNumber(totalChaptersString);


            var ratingSelector = ".rating .tag";
            var ratingString = document.QuerySelector(ratingSelector)!.TextContent;
            var rating = Utils.RatingDict[ratingString];

            var archiveWarningsSelector = ".warning .tag";
            var archiveWarningsString = document.QuerySelector(archiveWarningsSelector)!.TextContent;
            var archiveWarnings = archiveWarningsString.Split(", ")
                .Where(Utils.WarningDict.ContainsKey)
                .Select(t => Utils.WarningDict[t])
                .ToList();

            var categoriesSelector = "dd.category .tag";
            var categoriesEl = document.QuerySelectorAll(categoriesSelector);
            var categories = categoriesEl.Select(t => Utils.CategoryDict[t.TextContent]).ToList();

            var fandomsSelector = ".fandom .tag";
            var fandoms = document.QuerySelectorAll(fandomsSelector).Select(t => t.TextContent).ToList();

            var relationshipsSelector = ".relationship .tag";
            var relationships = document.QuerySelectorAll(relationshipsSelector).Select(t => t.TextContent).ToList();


            var charactersSelector = ".character .tag";
            var characters = document.QuerySelectorAll(charactersSelector).Select(t => t.TextContent).ToList();

            var completed = totalChapters == chapters;

            var textSelector = "#chapters";
            var text = document.QuerySelector(textSelector)!.TextContent;

            return new Work(id, title, summary, author, language, chapters, totalChapters, words, kudos, bookmarks, hits,
                            completed, updated, published, rating, archiveWarnings, categories, fandoms, relationships,
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

        public WorkMeta ToWorkMeta()
        {
            return new WorkMeta(Id, Title, Description, AuthorString, Language, CompletedChapters, TotalChapters, Updated ?? Published, Words, Kudos, Bookmarks, Hits, Completed, Rating, ArchiveWarnings, Categories, Fandoms, Relationships, Characters, FreeformTags);
        }
    }

}
