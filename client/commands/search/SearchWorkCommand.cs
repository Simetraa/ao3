using System.CommandLine;
using System.Reflection.Metadata.Ecma335;
using System.Web;
using ao3.commands;
using ao3.lib;
using ao3.lib.search;
using Spectre.Console;
using static System.Net.WebRequestMethods;

namespace ao3.client.commands.search
{
    public class SearchWorkCommand : Command
    {
        public SearchWorkCommand(Option<int> pageOption) : base("work", "Search for a work")
        {
            var searchWorkQuery = new Argument<string>(
                name: "query",
                description: "Searches all the fields associated with a work in the database, including summary, notes and tags, but not the full work text.",
                getDefaultValue: () => "");
            var searchWorkTitleOption = new Option<string>(
                aliases: ["--title"],
                description: "Search by title");
            var searchWorkAuthorOption = new Option<string>(
                aliases: ["--author"],
                description: "Search by author");
            var searchWorkDateOption = new Option<string>(
                aliases: ["--date"],
                description: "Search by date");
            var searchWorkCompleteOption = new Option<CompletionStatus>(
                aliases: ["--complete"],
                description: "Search by completion status");
            var searchWorkCrossoversOption = new Option<Crossovers>(
                aliases: ["--crossovers"],
                description: "Search by crossovers");
            var searchWorkSingleChapterOption = new Option<bool>(
                aliases: ["--single-chapter"],
                description: "Search by single chapter");
            var searchWorkLanguageOption = new Option<string>(
                aliases: ["--language"],
                description: "Search by language (en, fr, etc.)");
            var searchWorkFandomsOption = new Option<IEnumerable<string>>(
                aliases: ["--fandoms"],
                description: "Search by fandoms")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkRatingsOption = new Option<Rating>(
                aliases: ["--rating"],
                description: "Search by rating");
            var searchWorkWarningsOption = new Option<IEnumerable<Warning>>(
                aliases: ["--warnings"],
                description: "Search by warnings")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkCategoriesOption = new Option<IEnumerable<Category>>(
                aliases: ["--categories"],
                description: "Search by categories")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkCharactersOption = new Option<IEnumerable<string>>(
                aliases: ["--characters"],
                description: "Search by characters")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkRelationshipsOption = new Option<IEnumerable<string>>(
                aliases: ["--relationships"],
                description: "Search by relationships")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkAdditionalTagsOption = new Option<IEnumerable<string>>(
                aliases: ["--tags"],
                description: "Search by additional tags")
            { AllowMultipleArgumentsPerToken = true };
            var searchWorkMinWordsOption = new Option<int>(
                aliases: ["--min-words"],
                description: "Search by minimum words");
            var searchWorkMaxWordsOption = new Option<int>(
                aliases: ["--max-words"],
                description: "Search by maximum words");
            var searchWorkMinHitsOption = new Option<int>(
                aliases: ["--min-hits"],
                description: "Search by minimum hits");
            var searchWorkMaxHitsOption = new Option<int>(
                aliases: ["--max-hits"],
                description: "Search by maximum hits");
            var searchWorkMinKudosOption = new Option<int>(
                aliases: ["--min-kudos"],
                description: "Search by minimum kudos");
            var searchWorkMaxKudosOption = new Option<int>(
                aliases: ["--max-kudos"],
                description: "Search by maximum kudos");
            var searchWorkMinCommentsOption = new Option<int>(
                aliases: ["--min-comments"],
                description: "Search by minimum comments");
            var searchWorkMaxCommentsOption = new Option<int>(
                aliases: ["--max-comments"],
                description: "Search by maximum comments");
            var searchWorkMinBookmarksOption = new Option<int>(
                aliases: ["--min-bookmarks"],
                description: "Search by minimum bookmarks");
            var searchWorkMaxBookmarksOption = new Option<int>(
                aliases: ["--max-bookmarks"],
                description: "Search by maximum bookmarks");
            var searchWorkSortByOption = new Option<SortColumn>(
                aliases: ["--sort-by"],
                description: "Sort by");
            var searchWorkSortOrderOption = new Option<SortDirection>(
            aliases: ["--sort-order"],
            description: "Sort order");

            AddArgument(searchWorkQuery);
            AddOption(searchWorkTitleOption);
            AddOption(searchWorkAuthorOption);
            AddOption(searchWorkDateOption);
            AddOption(searchWorkCompleteOption);
            AddOption(searchWorkCrossoversOption);
            AddOption(searchWorkSingleChapterOption);
            AddOption(searchWorkLanguageOption);
            AddOption(searchWorkFandomsOption);
            AddOption(searchWorkRatingsOption);
            AddOption(searchWorkWarningsOption);
            AddOption(searchWorkCategoriesOption);
            AddOption(searchWorkCharactersOption);
            AddOption(searchWorkRelationshipsOption);
            AddOption(searchWorkAdditionalTagsOption);
            AddOption(searchWorkMinWordsOption);
            AddOption(searchWorkMaxWordsOption);
            AddOption(searchWorkMinHitsOption);
            AddOption(searchWorkMaxHitsOption);
            AddOption(searchWorkMinKudosOption);
            AddOption(searchWorkMaxKudosOption);
            AddOption(searchWorkMinCommentsOption);
            AddOption(searchWorkMaxCommentsOption);
            AddOption(searchWorkMinBookmarksOption);
            AddOption(searchWorkMaxBookmarksOption);
            AddOption(searchWorkSortByOption);
            AddOption(searchWorkSortOrderOption);




            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var query = context.ParseResult.GetValueForArgument(searchWorkQuery);
                var title = context.ParseResult.GetValueForOption(searchWorkTitleOption);
                var author = context.ParseResult.GetValueForOption(searchWorkAuthorOption);
                var date = context.ParseResult.GetValueForOption(searchWorkDateOption);
                var complete = context.ParseResult.GetValueForOption(searchWorkCompleteOption);
                var crossovers = context.ParseResult.GetValueForOption(searchWorkCrossoversOption);
                var singleChapter = context.ParseResult.GetValueForOption(searchWorkSingleChapterOption);
                var language = context.ParseResult.GetValueForOption(searchWorkLanguageOption);
                var fandoms = context.ParseResult.GetValueForOption(searchWorkFandomsOption);
                var ratings = context.ParseResult.GetValueForOption(searchWorkRatingsOption);
                var warnings = context.ParseResult.GetValueForOption(searchWorkWarningsOption);
                var categories = context.ParseResult.GetValueForOption(searchWorkCategoriesOption);
                var characters = context.ParseResult.GetValueForOption(searchWorkCharactersOption);
                var relationships = context.ParseResult.GetValueForOption(searchWorkRelationshipsOption);
                var additionalTags = context.ParseResult.GetValueForOption(searchWorkAdditionalTagsOption);
                var minWords = context.ParseResult.GetValueForOption(searchWorkMinWordsOption);
                var maxWords = context.ParseResult.GetValueForOption(searchWorkMaxWordsOption);
                var minHits = context.ParseResult.GetValueForOption(searchWorkMinHitsOption);
                var maxHits = context.ParseResult.GetValueForOption(searchWorkMaxHitsOption);
                var minKudos = context.ParseResult.GetValueForOption(searchWorkMinKudosOption);
                var maxKudos = context.ParseResult.GetValueForOption(searchWorkMaxKudosOption);
                var minComments = context.ParseResult.GetValueForOption(searchWorkMinCommentsOption);
                var maxComments = context.ParseResult.GetValueForOption(searchWorkMaxCommentsOption);
                var minBookmarks = context.ParseResult.GetValueForOption(searchWorkMinBookmarksOption);
                var maxBookmarks = context.ParseResult.GetValueForOption(searchWorkMaxBookmarksOption);
                var sortBy = context.ParseResult.GetValueForOption(searchWorkSortByOption);
                var sortOrder = context.ParseResult.GetValueForOption(searchWorkSortOrderOption);
                var page = context.ParseResult.GetValueForOption(pageOption);

                var searchQuery = new WorkSearch(query, title, author, date, complete, crossovers, singleChapter, language, fandoms, ratings, warnings, categories, characters, relationships, additionalTags, minWords, maxWords, minHits, maxHits, minKudos, maxKudos, minComments, maxComments, minBookmarks, maxBookmarks, sortBy, sortOrder);

                Console.WriteLine(searchQuery.GenerateSearchQuery());

                var results = await searchQuery.Search();

                foreach (var work in results.works)
                {
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
                }
            });
        }
    }
}
