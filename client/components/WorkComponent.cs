using ao3.lib.work;
using ao3.lib;
using Spectre.Console;

namespace ao3.client.widgets
{
    public class WorkComponent(WorkMeta work)
    {
        public WorkMeta Work { get; private set; } = work;
        public Table Render()
        {
            var ratingSymbol = Symbols.RatingSymbols[Work.Rating];

            Text categorySymbol;
            if (Work.Categories.Count() == 0)
            {
                categorySymbol = Symbols.CategorySymbols[Category.None];
            }
            else if (Work.Categories.Count() > 1)
            {
                categorySymbol = Symbols.CategorySymbols[Category.Multi];
            }
            else
            {
                categorySymbol = Symbols.CategorySymbols[Work.Categories.First()];
            }
            Text warningSymbol;

            if (Work.ArchiveWarnings.Contains(Warning.MajorCharacterDeath) || Work.ArchiveWarnings.Contains(Warning.GraphicDepictionsOfViolence))
            {
                warningSymbol = new Text("!");
            }
            else if (Work.ArchiveWarnings.Contains(Warning.CreatorChoseNotToUseArchiveWarnings))
            {
                warningSymbol = new Text("?");
            }
            else
            {
                warningSymbol = new Text(" ");
            }


            var completedSymbol = Work.Completed ? new Text("✓") : new Text("✘");

            var workUrl = $"https://archiveofourown.org/works/{Work.Id}".EscapeMarkup();
            var authorUrl = $"https://archiveofourown.org/users/{Work.AuthorString.Replace(" ", "_")}".EscapeMarkup();

            var authorString = Work.AuthorString.EscapeMarkup();
            var titleString = Work.Title.EscapeMarkup();
            var workDetails = Markup.FromInterpolated($"[link={workUrl}][red]{titleString}[/][/] by [link={authorUrl}][red][/]{authorString}[/] | [bold red3]ID: {Work.Id}[/] | [gray]Updated: {Work.Updated}[/]");

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

            var fandomsList = Work.Fandoms.Select(n => formatTagLink(n));

            var fandomsText = new Markup(string.Join(", ", fandomsList));

            headerTable.AddRow(ratingSymbol, categorySymbol, workDetails);
            headerTable.AddRow(warningSymbol, completedSymbol, fandomsText);


            var characterTagsList = Work.Characters.Select(n => formatTagLink(n));
            var freeformTagsList = Work.FreeformTags.Select(n => formatTagLink(n));
            var tagsList = characterTagsList.Concat(freeformTagsList);

            var tagsString = string.Join(", ", tagsList);

            var tags = new Markup(tagsString);

            workTable.AddRow(headerTable);
            workTable.AddRow(new Rule("[red]Tags[/]"));
            workTable.AddRow(tags);
            workTable.AddRow(new Rule("[red]Description[/]"));
            workTable.AddRow(Work.Description.EscapeMarkup());
            workTable.AddRow(new Rule("[red]Stats[/]"));
            workTable.AddRow(new Columns(
                new Text($"Language: {Work.Language}"),
                new Text($"Words: {Work.Words:n0}"),
            new Text($"Chapters: {Work.CompletedChapters}/{Work.TotalChapters?.ToString() ?? "?"}"),
            new Text($"Kudos: {Work.Kudos:n0}"),
                 new Text($"Hits: {Work.Hits:n0}")));


            return workTable;
        }
    }


}
