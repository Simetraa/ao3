using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace ao3.lib
{
    public class PeopleSearch
    {
        public PeopleSearch(string? query, List<string> names, List<string> fandoms)
        {
            Query = query;
            Names = names;
            Fandoms = fandoms;
        }

        string? Query { get; set; }


        List<string> Names { get; set; }


        List<string> Fandoms { get; set; }

        public string GenerateSearchQuery()
        {
            string url = "https://archiveofourown.org/people/search";




            url = QueryHelpers.AddQueryString(url, "people_search[query]", Query ?? "");
            url = QueryHelpers.AddQueryString(url, "people_search[fandom]", string.Join(",", Fandoms));
            url = QueryHelpers.AddQueryString(url, "people_search[name]", string.Join(",", Names));

            return url;
        }
    }
}
