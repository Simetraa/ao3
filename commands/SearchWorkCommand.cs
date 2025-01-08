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
    public class SearchWorkCommand : Command
    {
        public SearchWorkCommand() : base("work", "Search for a work")
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
            { AllowMultipleArgumentsPerToken = true};
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

            this.AddArgument(searchWorkQuery);
            this.AddOption(searchWorkTitleOption);
            this.AddOption(searchWorkAuthorOption);
            this.AddOption(searchWorkDateOption);
            this.AddOption(searchWorkCompleteOption);
            this.AddOption(searchWorkCrossoversOption);
            this.AddOption(searchWorkSingleChapterOption);
            this.AddOption(searchWorkLanguageOption);
            this.AddOption(searchWorkFandomsOption);
            this.AddOption(searchWorkRatingsOption);
            this.AddOption(searchWorkWarningsOption);
            this.AddOption(searchWorkCategoriesOption);
            this.AddOption(searchWorkCharactersOption);
            this.AddOption(searchWorkRelationshipsOption);
            this.AddOption(searchWorkAdditionalTagsOption);
            this.AddOption(searchWorkMinWordsOption);
            this.AddOption(searchWorkMaxWordsOption);
            this.AddOption(searchWorkMinHitsOption);
            this.AddOption(searchWorkMaxHitsOption);
            this.AddOption(searchWorkMinKudosOption);
            this.AddOption(searchWorkMaxKudosOption);
            this.AddOption(searchWorkMinCommentsOption);
            this.AddOption(searchWorkMaxCommentsOption);
            this.AddOption(searchWorkMinBookmarksOption);
            this.AddOption(searchWorkMaxBookmarksOption);
            this.AddOption(searchWorkSortByOption);
            this.AddOption(searchWorkSortOrderOption);




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

                Console.WriteLine($"Query: {query}\nTitle: {title}\nAuthor: {author}\nDate: {date}\nComplete: {complete}\nCrossovers: {crossovers}\nSingle Chapter: {singleChapter}\nLanguage: {language}\nFandoms: {fandoms}\nRatings: {ratings}\nWarnings: {warnings}\nCategories: {categories}\nCharacters: {characters}\nRelationships: {relationships}\nAdditional Tags: {additionalTags}\nMin Words: {minWords}\nMax Words: {maxWords}\nMin Hits: {minHits}\nMax Hits: {maxHits}\nMin Kudos: {minKudos}\nMax Kudos: {maxKudos}\nMin Comments: {minComments}\nMax Comments: {maxComments}\nMin Bookmarks: {minBookmarks}\nMax Bookmarks: {maxBookmarks}\nSort By: {sortBy}\nSort Order: {sortOrder}");

                var searchQuery = new WorkSearch(query, title, author, date, complete, crossovers, singleChapter, language, fandoms, ratings, warnings, categories, characters, relationships, additionalTags, minWords, maxWords, minHits, maxHits, minKudos, maxKudos, minComments, maxComments, minBookmarks, maxBookmarks, sortBy, sortOrder);
                
                
                Console.WriteLine(searchQuery.GenerateSearchQuery());

                var results = await searchQuery.Search();
                
                foreach(var work in results.works)
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

                    if(work.ArchiveWarnings.Contains(Warning.MajorCharacterDeath) || work.ArchiveWarnings.Contains(Warning.GraphicDepictionsOfViolence))
                    {
                        warningSymbol = new Text("!");
                    } else if (work.ArchiveWarnings.Contains(Warning.CreatorChoseNotToUseArchiveWarnings))
                    {
                        warningSymbol = new Text("?");
                    } else
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
                    headerTable.AddColumn();
                    

                    var updatedText = Markup.FromInterpolated($"[gray]{work.Updated}[/]");
                    var fandomsText = new Text(string.Join(", ", work.Fandoms));

                    headerTable.AddRow(ratingSymbol, categorySymbol, titleAndAuthor, updatedText);
                    headerTable.AddRow(warningSymbol, completedSymbol, fandomsText);


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
