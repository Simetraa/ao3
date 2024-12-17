using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Net.WebSockets;
using System.Text;
using ao3.commands;
using ao3.lib;
using ao3.lib.search;
using Spectre.Console;

namespace ao3.Commands
{
    public class SearchUserCommand : Command
    {
        public SearchUserCommand() : base("user", "Search for a user")
        {
            var searchUserQuery = new Argument<string>(
                name: "query",
                description: "Searches all the fields associated with a user in the database.");
            var searchNamesOption = new Option<IEnumerable<string>>(
                aliases: ["--names"],
                description: "Search by names")
            { AllowMultipleArgumentsPerToken = true };

            var searchFandomsOption = new Option<string>(
                aliases: ["--fandoms"],
                description: "Search by fandom");


            this.AddArgument(searchUserQuery);
            this.AddOption(searchNamesOption);
            this.AddOption(searchFandomsOption);





            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var query = context.ParseResult.GetValueForArgument(searchUserQuery);
                var names = context.ParseResult.GetValueForOption(searchNamesOption);
                var fandoms = context.ParseResult.GetValueForOption(searchFandomsOption);

                var userQuery = new PeopleSearch(query, names, fandoms)

                Console.WriteLine(searchQuery.GenerateSearchQuery());

                var results = await searchQuery.Search();

                foreach (var work in results.works)
                {
                    Console.WriteLine($"Currently printing {work.Id}");

                    //// Create a table
                    //var table = new Table();

                    //// Add some columns
                    //table.AddColumns(["ID,", "Title", "Author", "Description", "Fandoms", "Warnings", "Freeform Tags", "Language", "Words", "Chapters"]);

                    //table.AddRow(work.Id.ToString(), work.Title, work.AuthorString, work.Description, string.Join(", ", work.Fandoms), string.Join(", ", work.ArchiveWarnings), string.Join(", ", work.FreeformTags), work.Language, work.Words.ToString(), $"{work.CompletedChapters}/{work.CompletedChapters}");

                    //// Render the table to the console
                    //AnsiConsole.Write(table);

                    var ratingSymbol = Symbols.RatingSymbols[work.Rating];

                    var categorySymbol = work.Categories.Count() > 1 ? Symbols.CategorySymbols[Category.Multi] : Symbols.CategorySymbols[work.Categories.First()];


                    Text warningSymbol;

                    if (work.ArchiveWarnings.Contains(Warning.MajorCharacterDeath) || work.ArchiveWarnings.Contains(Warning.GraphicDepictionsOfViolence))
                    {
                        warningSymbol = new Text("!");
                    }
                    else if (work.ArchiveWarnings.Contains(Warning.CreatorChoseNotToUseArchiveWarnings))
                    {
                        warningSymbol = new Text("?");
                    }
                    else
                    {
                        warningSymbol = new Text(" ");
                    }


                    var completedSymbol = work.Completed ? new Text("✓") : new Text("✘");


                    var titleAndAuthor = Markup.FromInterpolated($"[red]{work.Title}[/] by [red]{work.AuthorString}[/]");

                    var workTable = new Table();
                    workTable.Expand();
                    workTable.HideHeaders();
                    workTable.Border = TableBorder.Rounded;

                    workTable.AddColumn("");


                    var symbolTable = new Table();
                    symbolTable.HideHeaders();
                    symbolTable.Border = TableBorder.None;
                    symbolTable.Collapse();

                    symbolTable.AddColumn("");
                    symbolTable.AddColumn("");

                    symbolTable.AddRow(ratingSymbol, categorySymbol);
                    symbolTable.AddRow(warningSymbol, completedSymbol);

                    //var headerTable = new Columns(
                    //    symbolTable.Centered(), new Rows(
                    //        titleAndAuthor,
                    //        new Text(string.Join(", ", work.Fandoms)))
                    //    ).Collapse();


                    var headerTable = new Grid();
                    headerTable.AddColumn();
                    headerTable.AddColumn();
                    headerTable.AddColumn();

                    headerTable.AddRow(ratingSymbol, categorySymbol, titleAndAuthor);
                    headerTable.AddRow(warningSymbol, completedSymbol, new Text(string.Join(", ", work.Fandoms)));


                    var characterTagsList = work.Characters.Select(n => $"[yellow]{n}[/]");
                    var freeformTagsList = work.FreeformTags.Select(n => $"[yellow]{n}[/]");
                    var tagsList = characterTagsList.Concat(freeformTagsList);

                    var tagsString = string.Join(", ", tagsList);

                    // TODO:  Make sure to escape each tag.
                    var tags = new Markup(tagsString);

                    workTable.AddRow(headerTable);
                    workTable.AddRow(new Rule("[red]Tags[/]"));
                    workTable.AddRow(tags);
                    workTable.AddRow(new Rule("[red]Description[/]"));
                    workTable.AddRow(work.Description);
                    workTable.AddRow(new Rule("[red]Stats[/]"));
                    workTable.AddRow(new Columns(
                        new Text($"Language: {work.Language}"),
                        new Text($"Words: {work.Words:n0}"),
                        new Text($"Chapters: {work.CompletedChapters}/{work.TotalChapters?.ToString() ?? "?"}"),
                        new Text($"Kudos: {work.Kudos:n0}"),
                        new Text($"Hits: {work.Hits:n0}")));


                    AnsiConsole.Write(workTable);
                }
            });
        }
    }
}
