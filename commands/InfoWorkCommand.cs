using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Net.WebSockets;
using System.Text;
using ao3.commands;
using ao3.lib;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.Commands
{
    public class InfoWorkCommand : Command
    {
        public InfoWorkCommand() : base("work", "Get info about a work")
        {
            var idArgument = new Argument<int>(
                name: "idArgument",
                description: "Searches all the fields associated with a work in the database, including summary, notes and tags, but not the full work text."
                );


            this.AddArgument(idArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var id = context.ParseResult.GetValueForArgument(idArgument);

                var work = await Work.ParseFromIdAsync(id);

 
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
            });
        }
    }
}
