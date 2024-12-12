using System.CommandLine;
using ao3.Commands;

//using System.Globalization;

namespace ao3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            args = ["search", "work", "--title", "Hi", "argument"];

            var rootCommand = new RootCommand();

            // search command
            var searchCommand = new Command("search", "Search AO3");

            // Add the search work command
            searchCommand.AddCommand(new SearchWorkCommand());

            rootCommand.AddCommand(searchCommand);

            await rootCommand.InvokeAsync(args);
        }

    }
}