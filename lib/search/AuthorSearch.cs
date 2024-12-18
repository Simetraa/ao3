using AngleSharp;
using AngleSharp.Dom;
using ao3.lib.author;
using Microsoft.AspNetCore.WebUtilities;

namespace ao3.lib.search
{
    public class AuthorSearch(string? query = "",
        IEnumerable<string>? names = null,
        IEnumerable<string>? fandoms = null)
    {
        string? Query { get; set; } = query;
        IEnumerable<string> Names { get; set; } = names ?? [];
        IEnumerable<string> Fandoms { get; set; } = fandoms ?? [];

        public string GenerateSearchQuery()
        {
            string url = "https://archiveofourown.org/people/search";

            url = QueryHelpers.AddQueryString(url, "people_search[query]", Query ?? "");

            foreach(var name in Names)
            {
                url = QueryHelpers.AddQueryString(url, "people_search[name]", name);
            }
            foreach (var fandom in Fandoms)
            {
                url = QueryHelpers.AddQueryString(url, "people_search[fandom]", fandom);
            }

            return url;
        }

        public static (int pageCount, int authorCount) ParseAuthorPageMeta(IDocument document)
        {
            var headingSelector = "p>strong";
            var heading = document.QuerySelector(headingSelector);
            var headingText = heading.TextContent;
            var headingRegex = new System.Text.RegularExpressions.Regex("([\\d,]+) Found"); // TODO: Make this more robust
            var match = headingRegex.Match(headingText);
            var authorCountString = match.Groups[1].Value;
            var authorCount = Utils.ParseNumber(authorCountString);

            var pageCountString = document.QuerySelector(".pagination li:has(+ .next) > a")?.TextContent ?? "1";

            var pageCount = Utils.ParseNumber(pageCountString);

            return (pageCount, authorCount);
        }

        public async Task<(int pageCount, int authorCount, IEnumerable<AuthorMeta> authors)> Search(int page = 1)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = GenerateSearchQuery();
            var context = BrowsingContext.New(config);

            address = QueryHelpers.AddQueryString(address, "page", page.ToString());


            var document = await context.OpenAsync(address);

            var authorElements = document.QuerySelectorAll("ol.index .user");

            var (pageCount, authorCount) = ParseAuthorPageMeta(document);

            IEnumerable<AuthorMeta> authors = authorElements.Select(AuthorMeta.ParseFromMeta);

            return (pageCount, authorCount, authors);
        }


    }
}
