using AngleSharp.Dom;
using AngleSharp;
using System.CommandLine;
using ao3.lib;
//using System.Globalization;

namespace ao3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            var work = await Work.ParseFromIdAsync(50865955);

            await work.Download(DownloadType.PDF);
            //var authorsWorks = await (await Author.ParseAsync("Lunik")).GetWorks();

            //Console.WriteLine(authorsWorks);

            //var search = new WorkSearch("Dracula", "title", "creators", "4 weeks ago", CompletionStatus.Complete,Crossovers.Include, false, "en",new List<string> { "Dracula" }, Rating.TeenAndUpAudiences, new List<string> { "Harry Potter"}, new List<string> { "Harry Potter/Dracula"}, new List<string> { }, null, null, null, null, null, null, null, null, null, null, null, null, SortColumn.BestMatch, SortDirection.Descending);
            //var query = search.GenerateSearchQuery();

            //var search = new PeopleSearch("", ["NiallWrites"], []);
            //var query = search.GenerateSearchQuery();

            //Console.WriteLine(query);


        }

    }
}