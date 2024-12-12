using System.CommandLine;
using ao3.lib;

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
            
            // include arguments and options
            this.SetHandler((query, title))

            //// Handler using query and option
            //this.SetHandler((string query, string title) =>
            //{
            //    Console.WriteLine($"test {query} {title}");
            //}, searchWorkQuery, searchWorkTitleOption);
        }
    }
}
