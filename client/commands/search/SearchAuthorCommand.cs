using System.CommandLine;
using ao3.lib.search;
using Spectre.Console;

namespace ao3.client.commands.search
{
    public class SearchAuthorCommand : Command
    {
        public SearchAuthorCommand(Option<int> pageOption) : base("author", "Search for a author")
        {
            var searchUserQuery = new Argument<string>(
                name: "query",
                description: "Searches all the fields associated with a user in the database.");
            var searchNamesOption = new Option<IEnumerable<string>>(
                aliases: ["--names"],
                description: "Search by names")
            { AllowMultipleArgumentsPerToken = true };

            var searchFandomsOption = new Option<IEnumerable<string>>(
                aliases: ["--fandoms"],
                description: "Search by fandom")
            { AllowMultipleArgumentsPerToken = true };


            AddArgument(searchUserQuery);
            AddOption(searchNamesOption);
            AddOption(searchFandomsOption);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var query = context.ParseResult.GetValueForArgument(searchUserQuery);
                var names = context.ParseResult.GetValueForOption(searchNamesOption);
                var fandoms = context.ParseResult.GetValueForOption(searchFandomsOption);

                var userQuery = new AuthorSearch(query, names, fandoms);

                Console.WriteLine(userQuery.GenerateSearchQuery());

                var (pageCount, authorCount, authors) = await userQuery.Search();
                var authorTable = new Table();
                authorTable.Expand();
                authorTable.AddColumn("Name");
                authorTable.AddColumn("Pseud");
                authorTable.AddColumn("Works");
                authorTable.AddColumn("Bookmarks");

                foreach (var author in authors)
                {
                    authorTable.AddRow(author.Name, author.Pseud ?? "", author.Works.ToString(), author.Bookmarks.ToString());
                }

                AnsiConsole.Write(authorTable);

            });
        }
    }
}
