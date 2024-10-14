using AngleSharp.Dom;
using AngleSharp;
using System.Globalization;

namespace ao3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //var work = await Work.ParseFromWorkAsync("50865955");

            //var authorsWorks = await (await Author.ParseAsync("Lunik")).GetWorks();
            
            //Console.WriteLine(authorsWorks);

            var search = new WorkSearch(1000, 10000, 100, 1000, 10, 100, 10, 1000);
            var query = search.GenerateSearchQuery();
            Console.WriteLine(query);
        }
    }
}