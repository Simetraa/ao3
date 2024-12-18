using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Net.WebSockets;
using System.Text;
using ao3.commands;
using ao3.lib;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.Commands
{
    public class DownloadWorkCommand : Command
    {
        public DownloadWorkCommand(Option<int> threadsOption, Option<DownloadType> formatOption) : base("work", "Get info about a work")
        {
            var idArgument = new Argument<IEnumerable<int>>(
                      name: "id",
                      description: "The IDs of the works to download");

            this.AddArgument(idArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var idList = context.ParseResult.GetValueForArgument(idArgument);
                var threads = context.ParseResult.GetValueForOption(threadsOption);
                var format = context.ParseResult.GetValueForOption(formatOption);

                //first download the Work data concurrently, using thread count
                // then, get the download urls using the Work.Download() method.

                Console.WriteLine(value: $"{idList} ${threads} ${format}-");

                // First download the Work data concurrently, using thread count
                var workTasks = idList.Select(id => Work.ParseFromIdAsync(id)).ToList();
                var works = await Task.WhenAll(workTasks);

                // Then, get the download URLs using the Work.Download() method
                var downloadTasks = works.Select(work => work.Download(format, "%title%"));
                await Task.WhenAll(downloadTasks);


                Console.Write($"Downloading {idList.Count()} works with {threads} threads in {format} format");

            });
        }
    }
}
