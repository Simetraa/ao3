using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Net.WebSockets;
using System.Text;
using ao3.commands;
using ao3.lib;
using ao3.lib.author;
using ao3.lib.search;
using ao3.lib.work;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace ao3.Commands
{
    public class DownloadAuthorCommand : Command
    {
        public DownloadAuthorCommand(Option<int> threadsOption, Option<DownloadType> formatOption, Option<string> outputOption) : base("author", "Get info about a work")
        {
            var nameArgument = new Argument<string>(
                      name: "names",
                      description: "The names of the author to download works from.");

            this.AddArgument(nameArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var name = context.ParseResult.GetValueForArgument(nameArgument);
                var threads = context.ParseResult.GetValueForOption(threadsOption);
                var format = context.ParseResult.GetValueForOption(formatOption);
                string output = context.ParseResult.GetValueForOption(outputOption) ?? "";

                ParallelOptions options = new()
                {
                    MaxDegreeOfParallelism = threads
                };

                await AnsiConsole.Progress()
                    .StartAsync(async ctx =>
                    {
                        ProgressTask? task = ctx.AddTask("[green]Downloading author...[/]").MaxValue(1);
                        while (!ctx.IsFinished)
                        {
                            var author = await Author.ParseAsync(name);
                            var (pageCount, workCount, works) = await author.GetWorks();


                            var pageTask = ctx.AddTask($"[red]Downloading page...[/]").MaxValue(pageCount);

                            // for each page
                            await Parallel.ForAsync(0, pageCount, options, async (work, token2) =>
                            {
                                var (pageCount, workCount, works) = await author.GetWorks();
                                pageTask.Increment(1);
                                var workTask = ctx.AddTask($"[blue]Downloading work...[/]").MaxValue(works.Count());
                                // for each book
                                await Parallel.ForEachAsync(works, options, async (work, token3) =>
                                {
                                    await work.Download(format, output);
                                    workTask.Increment(1);
                                });
                            });
                            task.Increment(1);
                        }
                    });
            });
        }
    }
}