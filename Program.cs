using System.CommandLine;
using ao3.lib;
using System.Text;
using ao3.client.commands.download;
using ao3.client.commands.watch;
using ao3.client.commands.info;
using ao3.client.commands.search;

namespace ao3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;

            //args = ["watch", "remove", "62104588"];
            var rootCommand = new RootCommand();

            // search
            var searchCommand = new Command("search", "Search AO3");

            var pageOption = new Option<int>
                (name: "--page",
                description: "Page to search on",
                getDefaultValue: () => 1);

            searchCommand.AddGlobalOption(pageOption);

            searchCommand.AddCommand(new SearchWorkCommand(pageOption));
            searchCommand.AddCommand(new SearchAuthorCommand(pageOption));

            // info
            var infoCommand = new Command("info", "Get info from AO3");
            infoCommand.AddCommand(new InfoWorkCommand());
            infoCommand.AddCommand(new InfoAuthorCommand());
            
            // download
            var downloadCommand = new Command("download", "Download a work from ao3");

            var threadsOption = new Option<int>
                (name: "--threads",
                description: "How many concurrent downloads can be performed. Decrease if rate limited",
                getDefaultValue: () => 10);

            var outputOption = new Option<string>
                (name: "--output",
                description: "Output format for downloaded files",
                getDefaultValue: () => "%author% - %title%.%ext%");

            var formatOption = new Option<DownloadType>
                (name: "--format",
                description: "Format to download files in.",
                getDefaultValue: () => DownloadType.HTML);

            downloadCommand.AddGlobalOption(threadsOption);
            downloadCommand.AddGlobalOption(formatOption);
            downloadCommand.AddGlobalOption(outputOption);
            downloadCommand.AddGlobalOption(threadsOption);

            downloadCommand.AddCommand(new DownloadWorkCommand(threadsOption, formatOption, outputOption));
            downloadCommand.AddCommand(new DownloadAuthorCommand(threadsOption, formatOption, outputOption));

            // watch
            var watchCommand = new Command("watch", "Watch works from AO3");
            watchCommand.AddCommand(new WatchAddCommand());
            watchCommand.AddCommand(new WatchListCommand());
            watchCommand.AddCommand(new WatchUpdateCommand());
            watchCommand.AddCommand(new WatchRemoveCommand());


            rootCommand.AddCommand(searchCommand);
            rootCommand.AddCommand(infoCommand);
            rootCommand.AddCommand(downloadCommand);
            rootCommand.AddCommand(watchCommand);

            await rootCommand.InvokeAsync(args);
        }
    }
}