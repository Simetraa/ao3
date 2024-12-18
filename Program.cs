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

            args = ["download", "work", "12891420", "12279219"];

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

            var formatOption = new Option<DownloadType>
                (name: "--format",
                description: "Format to download files in.",
                getDefaultValue: () => DownloadType.HTML);
            downloadCommand.AddGlobalOption(formatOption);


            downloadCommand.AddCommand(new DownloadWorkCommand(threadsOption, formatOption));
            //downloadCommand.AddCommand(new DownloadAuthorCommand(threadsOption, formatOption));

            rootCommand.AddCommand(searchCommand);
            rootCommand.AddCommand(infoCommand);
            rootCommand.AddCommand(downloadCommand);



            await rootCommand.InvokeAsync(args);









        }

    }
}