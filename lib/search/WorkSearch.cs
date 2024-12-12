using AngleSharp;
using AngleSharp.Dom;
using ao3.lib.work;
using Microsoft.AspNetCore.WebUtilities;

namespace ao3.lib.search
{
    public class WorkSearch(string query,
                      string title,
                      string creators,
                      string date,
                      CompletionStatus complete,
                      Crossovers crossovers,
                      bool onlySingleChapter,
                      string language,
                      List<string> fandoms,
                      Rating? rating,
                      List<Warning> warnings,
                      List<Category> categories,
                      List<string> characters,
                      List<string> relationships,
                      List<string> additionalTags,
                      int? minWords,
                      int? maxWords,
                      int? minHits,
                      int? maxHits,
                      int? minKudos,
                      int? maxKudos,
                      int? minComments,
                      int? maxComments,
                      int? minBookmarks,
                      int? maxBookmarks,
                      SortColumn sortBy,
                      SortDirection sortOrder
            )
    {
        public string GenerateSearchQuery()
        {
            string url = "https://archiveofourown.org/works/search";

            url = QueryHelpers.AddQueryString(url, "work_search[query]", Query ?? "");
            url = QueryHelpers.AddQueryString(url, "work_search[title]]", Title);
            url = QueryHelpers.AddQueryString(url, "work_search[creators]", Creators);
            url = QueryHelpers.AddQueryString(url, "work_search[revised_at]]", Date);
            url = QueryHelpers.AddQueryString(url, "work_search[complete]]", Convert.ToString((int)Complete));
            url = QueryHelpers.AddQueryString(url, "work_search[crossover]]", Convert.ToString((int)Crossovers));
            url = QueryHelpers.AddQueryString(url, "work_search[single_chapter]]", OnlySingleChapter ? "T" : "F");
            url = QueryHelpers.AddQueryString(url, "work_search[language_id]]", Language);
            url = QueryHelpers.AddQueryString(url, "work_search[fandom_names]", string.Join(",", Fandoms));
            foreach (var warning in Warnings)
            {
                url = QueryHelpers.AddQueryString(url, "work_search[archive_warning_ids][]", Convert.ToString((int)warning));
            }


            foreach (var category in Categories)
            {
                url = QueryHelpers.AddQueryString(url, "work_search[category_ids][]", Convert.ToString((int)category));
            }

            if (Rating.HasValue) // find a better way than this
            {
                url = QueryHelpers.AddQueryString(url, "work_search[rating_ids]", Convert.ToString((int)Rating));
            }
            else
            {
                url = QueryHelpers.AddQueryString(url, "work_search[rating_ids]", ""); // TODO: This is really messy.

            }

            url = QueryHelpers.AddQueryString(url, "work_search[character_names]", string.Join(",", Characters));
            url = QueryHelpers.AddQueryString(url, "work_search[relationship_names]", string.Join(",", Relationships));
            url = QueryHelpers.AddQueryString(url, "work_search[freeform_names]", string.Join(",", AdditionalTags));
            url = QueryHelpers.AddQueryString(url, "work_search[word_count]", Utils.FormatRange(MinWords, MaxWords));
            url = QueryHelpers.AddQueryString(url, "work_search[hits]", Utils.FormatRange(MinHits, MaxHits));
            url = QueryHelpers.AddQueryString(url, "work_search[kudos_count]", Utils.FormatRange(MinKudos, MaxKudos));
            url = QueryHelpers.AddQueryString(url, "work_search[comments_count]", Utils.FormatRange(MinComments, MaxComments));
            url = QueryHelpers.AddQueryString(url, "work_search[bookmarks_count]", Utils.FormatRange(MinBookmarks, MaxBookmarks));

            switch (SortBy)
            {
                case SortColumn.BestMatch:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "_score");
                    break;
                case SortColumn.Author:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "authors_to_sort_on");
                    break;
                case SortColumn.Title:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "title_to_sort_on");
                    break;
                case SortColumn.DatePosted:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "created_at");
                    break;
                case SortColumn.DateUpdated:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "revised_at");
                    break;
                case SortColumn.WordCount:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "word_count");
                    break;
                case SortColumn.Hits:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "hits");
                    break;
                case SortColumn.Kudos:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "kudos_count");
                    break;
                case SortColumn.Comments:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "comments_count");
                    break;
                case SortColumn.Bookmarks:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "bookmarks_count");
                    break;
            }

            switch (SortOrder)
            {
                case SortDirection.Descending:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_direction]", "desc");
                    break;
                case SortDirection.Ascending:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_direction]", "asc");
                    break;
            }

            switch (Complete)
            {
                case CompletionStatus.All:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "");
                    break; // we may be able to remove this
                case CompletionStatus.Complete:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "T");
                    break;
                case CompletionStatus.InProgress:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "F");
                    break;
            }

            return url;
        }

        public static (int pageCount, int workCount) ParseWorkPageMeta(IDocument document)
        {
            var heading = document.QuerySelector("h3.heading")!.TextContent;
            var headingRegex = new System.Text.RegularExpressions.Regex("([\\d,]+) Found");
            var match = headingRegex.Match(heading);
            var workCount = int.Parse(match.Groups[1].Value);

            var pageCountEl = document.QuerySelector(".pagination li:has(+ .next) > a")!.TextContent;
            var pageCount = int.Parse(pageCountEl);

            return (pageCount, workCount);
        }

        public async Task<(int pageCount, int workCount, IEnumerable<WorkMeta> works)> Search(int page = 1)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = GenerateSearchQuery();
            var context = BrowsingContext.New(config);

            address = QueryHelpers.AddQueryString(address, "page", page.ToString());


            var document = await context.OpenAsync(address);

            var workElements = document.QuerySelectorAll("li.work");

            var (pageCount, workCount) = ParseWorkPageMeta(document);

            IEnumerable<WorkMeta> works = workElements.Select(WorkMeta.ParseFromMeta);

            return (pageCount, workCount, works);
        }



        string Query { get; set; } = query;
        string Title { get; set; } = title;
        string? Creators { get; set; } = creators;
        string Date { get; set; } = date;
        CompletionStatus Complete { get; set; } = complete;
        Crossovers Crossovers { get; set; } = crossovers;
        bool OnlySingleChapter { get; set; } = onlySingleChapter;
        string Language { get; set; } = language;
        List<string> Fandoms { get; set; } = fandoms;
        Rating? Rating { get; set; } = rating;
        List<Warning> Warnings { get; set; } = warnings;
        List<Category> Categories { get; set; } = categories;
        List<string> Characters { get; set; } = characters;
        List<string> Relationships { get; set; } = relationships;
        List<string> AdditionalTags { get; set; } = additionalTags;

        int? MinWords { get; set; } = minWords;
        int? MaxWords { get; set; } = maxWords;
        int? MinHits { get; set; } = minHits;
        int? MaxHits { get; set; } = maxHits;
        int? MinKudos { get; set; } = minKudos;
        int? MaxKudos { get; set; } = maxKudos;
        int? MinComments { get; set; } = minComments;
        int? MaxComments { get; set; } = maxComments;
        int? MinBookmarks { get; set; } = minBookmarks;
        int? MaxBookmarks { get; set; } = maxBookmarks;
        SortColumn SortBy { get; set; } = sortBy;
        SortDirection SortOrder { get; set; } = sortOrder;




    }



}
