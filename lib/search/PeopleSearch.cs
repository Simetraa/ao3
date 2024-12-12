using Microsoft.AspNetCore.WebUtilities;

namespace ao3.lib.search
{
    public class PeopleSearch(string? query, List<string> names, List<string> fandoms)
    {
        string? Query { get; set; } = query;
        List<string> Names { get; set; } = names;
        List<string> Fandoms { get; set; } = fandoms;

        public string GenerateSearchQuery()
        {
            string url = "https://archiveofourown.org/people/search";

            url = QueryHelpers.AddQueryString(url, "people_search[query]", Query ?? "");
            url = QueryHelpers.AddQueryString(url, "people_search[fandom]", string.Join(",", Fandoms));
            url = QueryHelpers.AddQueryString(url, "people_search[name]", string.Join(",", Names));

            return url;
        }

        //public async Task<(int pageCount, int workCount, IEnumerable<WorkMeta> works)> Search(int page = 1)
        //{
        //    var config = Configuration.Default.WithDefaultLoader();
        //    var address = GenerateSearchQuery();
        //    var context = BrowsingContext.New(config);

        //    address = QueryHelpers.AddQueryString(address, "page", page.ToString());


        //    var document = await context.OpenAsync(address);

        //    var workElements = document.QuerySelectorAll("li.work");

        //    var (pageCount, workCount) = ParseWorkMeta(document);

        //    IEnumerable<WorkMeta> works = workElements.Select(WorkMeta.ParseFromMeta);

        //    return (pageCount, workCount, works);
        //}


    }
}
