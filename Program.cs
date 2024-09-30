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

            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/users/Killerfuzzel/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var joinDateSelector = "dl[class='meta'] > dd:nth-child(4)";
            var joinDateString = document.QuerySelector(joinDateSelector).Text();
            var joinDate = DateTime.ParseExact(joinDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;


            var userIdSelector = "dl[class='meta'] > dd:nth-child(6)";
            var userIdString = document.QuerySelector(userIdSelector).Text();

            var work

            Console.WriteLine(userIdString);
            Console.WriteLine(joinDate.ToString());
        }
    }
}