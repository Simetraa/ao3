using AngleSharp;

namespace ao3
{
    public class Work
    {
        public Work(int id, string title, string description, string author, string language, int completedChapters, int? totalChapters, int words, int kudos, int? bookmarks, int hits, bool completed, DateOnly? updated, DateOnly? published, Rating rating, Warning archiveWarning, Category category, List<string> fandoms, List<string> relationships, List<string> characters, List<string> tags, string? text)
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
            CompletedChapters = completedChapters;
            TotalChapters = totalChapters;
            Kudos = kudos;
            Bookmarks = bookmarks;
            Hits = hits;
            ArchiveWarning = archiveWarning;
            Category = category;
            Fandoms = fandoms;
            Relationships = relationships;
            Characters = characters;
            Completed = completed;
            Rating = rating;
            Text = text;
        }

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
        public List<string> Tags { get; }
        public string Language { get; }
        public int Words { get; }
        public int CompletedChapters { get; }
        public int? TotalChapters { get; }
        public int Kudos { get; }
        public int? Bookmarks { get; } // eventually create classes for Kudos & Bookmarks
        public int Hits { get; }
        public string? Text { get; }

        public async Task<Author> GetAuthor()
        {
            return await Author.ParseAsync(AuthorString);
        }

        public static Work ParseFromMeta(AngleSharp.Dom.IElement html)
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

            var ratingSelector = ".required-freeformTags .rating span";
            var ratingsString = html.QuerySelector(ratingSelector)!.TextContent;
            var rating = Utils.RatingDict[ratingsString];

            var archiveWarningSelector = ".required-freeformTags .warnings span";
            var archiveWarningString = html.QuerySelector(archiveWarningSelector)!.TextContent;
            var archiveWarning = Utils.WarningDict[archiveWarningString];

            var categorySelector = ".required-freeformTags .category span";
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

            var completedSelector = ".required-freeformTags .iswip span";
            var completedString = html.QuerySelector(completedSelector)!.TextContent;
            var completed = completedString == "Complete Work";

            return new Work(id, title, summary, author, language, chapters, totalChapters, words, kudos, null, hits,
                            completed, updated, null, rating, archiveWarning, category, fandoms, relationships,
                            characters, freeformTags, null);
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
            var publishedString = document.QuerySelector(publishedSelector).TextContent;
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


            return new Work(id, title, summary, author, language, chapters, totalChapters, words, kudos, null, hits,
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
