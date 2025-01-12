using System.CommandLine;
using ao3.lib;
using ao3.lib.author;
using Spectre.Console;

namespace ao3.client.commands.download
{
    public class DownloadAuthorCommand : Command
    {
        public DownloadAuthorCommand(Option<int> threadsOption, Option<DownloadType> formatOption, Option<string> outputOption) : base("author", "Download all works from an author")
        {
            var nameArgument = new Argument<string>(
                      name: "names",
                      description: "The names of the author to download works from.");

            AddArgument(nameArgument);

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
                            var (pageCount, _, _) = await author.GetWorks(); // get page count

                            var pageTask = ctx.AddTask($"[red]Downloading pages...[/]").MaxValue(pageCount);

                            // for each page
                            await Parallel.ForAsync(1, pageCount, options, async (workIndex, token2) =>
                            {
                                var (pageCount, workCount, works) = await author.GetWorks(workIndex); // download current page
                                pageTask.Increment(1);
                                var workTask = ctx.AddTask($"[blue]Downloading works...[/]").MaxValue(works.Count());
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