using System.CommandLine;
using ao3.commands;
using ao3.lib;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.info
{
    public class InfoWorkCommand : Command
    {
        public InfoWorkCommand() : base("work", "Get info about a work")
        {
            var idArgument = new Argument<int>(
                name: "id"
                );


            AddArgument(idArgument);

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

                var workUrl = $"https://archiveofourown.org/works/{work.Id}".EscapeMarkup();
                var authorUrl = $"https://archiveofourown.org/users/{work.AuthorString.Replace(" ", "_")}".EscapeMarkup();

                var authorString = work.AuthorString.EscapeMarkup();
                var titleString = work.Title.EscapeMarkup();

                var workDetails = Markup.FromInterpolated($"[link={workUrl}][red]{titleString}[/][/] by [link={authorUrl}][red][/]{authorString}[/] #{work.Id} [gray]{work.Updated}[/]");

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

                var headerTable = new Grid();
                headerTable.AddColumn();
                headerTable.AddColumn();
                headerTable.AddColumn();

                static string formatTagLink(string n) => $"[link=https://archiveofourown.org/tags/{Uri.EscapeDataString(n)}][yellow]{n}[/][/]";

                var fandomsList = work.Fandoms.Select(n => formatTagLink(n));

                var fandomsText = new Markup(string.Join(", ", fandomsList));

                headerTable.AddRow(ratingSymbol, categorySymbol, workDetails);
                headerTable.AddRow(warningSymbol, completedSymbol, fandomsText);


                var characterTagsList = work.Characters.Select(n => formatTagLink(n));
                var freeformTagsList = work.FreeformTags.Select(n => formatTagLink(n));
                var tagsList = characterTagsList.Concat(freeformTagsList);

                var tagsString = string.Join(", ", tagsList);

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
