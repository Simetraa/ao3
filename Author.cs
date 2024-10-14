using System;//
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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


        public static async Task<Author> ParseAsync(string id)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/users/Killerfuzzel/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var selector = "dl[class='meta'] > dd:nth-child(4)";
            var cells = document.QuerySelector(selector);

            return new Author("", cells.TextContent);
            // #workskin > div.preface.group > h3 > a
        }


        public async Task<List<Work>> GetWorks()
        {
            // https://archiveofourown.org/users/Lunik/pseuds/Lunik/works
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://archiveofourown.org/users/Lunik/pseuds/Lunik/works";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);


            var workElements = document.QuerySelectorAll("li.work");
            List<Work> works = new();

            foreach (var item in workElements)
            {
                works.Add(Work.ParseFromMeta(item));
            }


            return works;

    

        }


    }


}
