using System.CommandLine;
using ao3.client.widgets;
using ao3.lib.author;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.search
{
    public class SearchAuthorCommand : Command
    {
        public SearchAuthorCommand(Option<int> pageOption) : base("author", "Search for a author")
        {
            var searchAuthorQuery = new Argument<string>(
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


            AddArgument(searchAuthorQuery);
            AddOption(searchNamesOption);
            AddOption(searchFandomsOption);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var query = context.ParseResult.GetValueForArgument(searchAuthorQuery);
                var names = context.ParseResult.GetValueForOption(searchNamesOption);
                var fandoms = context.ParseResult.GetValueForOption(searchFandomsOption);
                var page = context.ParseResult.GetValueForOption(pageOption);
                try
                {
                    var authorQuery = new AuthorSearch(query, names, fandoms);
                    var authorQueryUrl = authorQuery.GenerateSearchQuery();

                    var (pageCount, authorCount, authors) = await authorQuery.Search(page);
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
                    AnsiConsole.Write(new PageWidget<AuthorMeta>(page, pageCount, authorCount, authors, authorQueryUrl).Render());
                }
                catch (Exception e)
                {
                    AnsiConsole.WriteException(e);

                }
            });
        }
    }
}
