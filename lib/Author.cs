using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AngleSharp;

namespace ao3.lib
{
    public class Author(string name, string id, string dateJoined)
    {
        public string Name { get; } = name;
        public string DateJoined { get; } = dateJoined;

        public string Id { get; } = id;


        public static async Task<Author> ParseAsync(string name)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"https://archiveofourown.org/users/{name}/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var selector = "dl[class='meta'] > dd:nth-child(4)";
            var cells = document.QuerySelector(selector);

            var nameSelector = "dl[class='meta'] > dd:nth-of-type(1)";
            var _ = document.QuerySelector(nameSelector)!.TextContent;

            var dateJoinedSelector = "dl[class='meta'] > dd:nth-of-type(2)";
            var dateJoined = document.QuerySelector(dateJoinedSelector)!.TextContent;

            var idSelector = "dl[class='meta'] > dd:nth-of-type(3)";
            var id = document.QuerySelector(idSelector)!.TextContent;

            return new Author(name, id, dateJoined);
        }


        public async Task<(int pageCount, int workCount, IEnumerable<WorkMeta> works)> GetWorks(int page = 1)
        {
            var search = new WorkSearch("", "", Name, "", CompletionStatus.All, Crossovers.Include, false, "", [],
                                        null, [], [], [], [], [], null, null, null, null, null, null, null, null, null,
                                        null, SortColumn.DatePosted, SortDirection.Descending);
            (int pageCount, int workCount, IEnumerable<WorkMeta> works) = await search.Search(page);


            return (pageCount, workCount, works);
        }
    }
}