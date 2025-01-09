using System.CommandLine;
using ao3.lib;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.download
{
    public class DownloadWorkCommand : Command
    {
        public DownloadWorkCommand(Option<int> threadsOption, Option<DownloadType> formatOption, Option<string> outputOption) : base("work", "Download a list of works")
        {
            var idArgument = new Argument<IEnumerable<int>>(
                      name: "id",
                      description: "The IDs of the works to download");

            AddArgument(idArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var idList = context.ParseResult.GetValueForArgument(idArgument);
                var threads = context.ParseResult.GetValueForOption(threadsOption);
                var output = context.ParseResult.GetValueForOption(outputOption);
                var format = context.ParseResult.GetValueForOption(formatOption);

                //first download the Work data concurrently, using thread count
                // then, get the download urls using the Work.Download() method.

                //Console.WriteLine(value: $"{idList} ${threads} ${format}-");

                ParallelOptions options = new()
                {
                    MaxDegreeOfParallelism = threads
                };

                await AnsiConsole.Progress()
                    .StartAsync(async ctx =>
                    {
                        var task = ctx.AddTask("[green]Download works...[/]").MaxValue(idList.Count());

                        while (!ctx.IsFinished)
                        {
                            await Parallel.ForEachAsync(idList, options, async (id, token) =>
                            {
                                var work = await Work.ParseFromIdAsync(id);
                                task.Increment(.5);
                                await work.Download(format, output);
                                task.Increment(.5);

                            });
                        }
                    });



                Console.Write($"Downloading {idList.Count()} works with {threads} threads in {format} format");
            });
        }
    }
}
