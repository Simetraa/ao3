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
    public class DownloadAuthorCommand : Command
    {
        public DownloadAuthorCommand(Option<int> threadsOption, Option<DownloadType> formatOption) : base("author", "Get info about a work")
        {
            var nameArgument = new Argument<string>(
                      name: "name",
                      description: "The name of the author to download works from.");

            this.AddArgument(nameArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var name = context.ParseResult.GetValueForArgument(nameArgument);
                var threads = context.ParseResult.GetValueForOption(threadsOption);
                var format = context.ParseResult.GetValueForOption(formatOption);


                Console.Write($"Downloading {name.Count()} works with {threads} threads in {format} format");

            });
        }
    }
}
