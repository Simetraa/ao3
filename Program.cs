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

            args = ["search", "work", "anonymous"];

            var rootCommand = new RootCommand();

            var searchCommand = new Command("search", "Search AO3");
            searchCommand.AddCommand(new SearchWorkCommand());
            searchCommand.AddCommand(new SearchAuthorCommand());

            var infoCommand = new Command("info", "Get info from AO3");
            infoCommand.AddCommand(new InfoWorkCommand());
            infoCommand.AddCommand(new InfoAuthorCommand());

            var downloadCommand = new Command("download", "Get info from AO3");

            var threadsOption = new Option<int>
                (name: "--threads",
                description: "How many concurrent downloads can be performed. Decrease if rate limited",
                getDefaultValue: () => 10);
            downloadCommand.AddGlobalOption(threadsOption);

            var outputOption = new Option<string>
                (name: "--output",
                description: "Output format for downloaded files",
                getDefaultValue: () => "%author% - %title%.%ext%");

            var formatOption = new Option<DownloadType>
                (name: "--format",
                description: "Format to download files in.",
                getDefaultValue: () => DownloadType.HTML);


            downloadCommand.AddGlobalOption(formatOption);
            downloadCommand.AddGlobalOption(outputOption);
            downloadCommand.AddGlobalOption(threadsOption);

            downloadCommand.AddCommand(new DownloadWorkCommand(threadsOption, formatOption, outputOption));
            downloadCommand.AddCommand(new DownloadAuthorCommand(threadsOption, formatOption, outputOption));

            rootCommand.AddCommand(searchCommand);
            rootCommand.AddCommand(infoCommand);
            rootCommand.AddCommand(downloadCommand);



            await rootCommand.InvokeAsync(args);









        }

    }
}