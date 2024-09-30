using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AngleSharp;

namespace ao3
{
    internal class Author
    {
        public Author(string name, string dateJoined)
        {
            Name = name;
            DateJoined = dateJoined;
        }

        public string Name { get; } // we do not have to declare a setter as user will not be setting these values
        public string DateJoined { get; }


        public static async Task<Author> ParseAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/users/Killerfuzzel/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            Console.WriteLine(document.ToHtml());
            var selector = "dl[class='meta'] > dd:nth-child(4)";
            var cells = document.QuerySelector(selector);

            Console.WriteLine(cells.TextContent);

            return new Author(url, cells.TextContent);
            // #workskin > div.preface.group > h3 > a
        }
    }


}
