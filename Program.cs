using System.CommandLine;
using ao3.Commands;
using ao3.lib.search;
using ao3.lib;
using System.Text;
using Spectre.Console;

//using System.Globalization;

namespace ao3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;

            args = ["info", "work", "57149356"];

            var rootCommand = new RootCommand();

            // search command
            var searchCommand = new Command("search", "Search AO3");

            // Add the search work command
            searchCommand.AddCommand(new SearchWorkCommand());

            var infoCommand = new Command("info", "Get info from AO3");
            infoCommand.AddCommand(new InfoWorkCommand());

            rootCommand.AddCommand(searchCommand);
            rootCommand.AddCommand(infoCommand);

            await rootCommand.InvokeAsync(args);









        }

    }
}