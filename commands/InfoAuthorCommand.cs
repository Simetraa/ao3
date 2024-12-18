using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Net.WebSockets;
using System.Text;
using ao3.commands;
using ao3.lib;
using ao3.lib.author;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.Commands
{
    public class InfoAuthorCommand : Command
    {
        public InfoAuthorCommand() : base("author", "Get info about a author")
        {
            var nameArgument = new Argument<string>(
                name: "name"
            );

            var avatarOption = new Option<bool>(
                aliases: ["--no-avatar"],
                getDefaultValue: () => false
            );


            this.AddArgument(nameArgument);
            this.AddOption(avatarOption);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var name = context.ParseResult.GetValueForArgument(nameArgument);
                var hideAvatar = context.ParseResult.GetValueForOption(avatarOption);

                var author = await Author.ParseAsync(name);


                if (hideAvatar)
                {
                    var grid = new Table();
                    grid.HideHeaders();
                    grid.AddColumn("");
                    var panel = new Rows(
                                          Markup.FromInterpolated($"Name: {author.Name}"),
                                          Markup.FromInterpolated($"Pseuds: {string.Join(", ", author.Pseuds)}"),
                                          Markup.FromInterpolated($"Join Date: {author.DateJoined}"),
                                          Markup.FromInterpolated($"ID: {author.Id}"),
                                          Markup.FromInterpolated($"Location: {author.Location}"),
                                          new Rule("[red]Bio[/]"),
                                          new Text(author.Bio));

                    grid.AddRow(panel);

                    AnsiConsole.Write(grid);

                }
                else
                {
                    var grid = new Table();
                    grid.HideHeaders();
                    grid.AddColumn("");
                    grid.AddColumn("");

                    var image = new CanvasImage(await author.DownloadAvatar());
                    image.MaxWidth(16);

                    var panel = new Rows(
                                          Markup.FromInterpolated($"Name: {author.Name}"),
                                          Markup.FromInterpolated($"Pseuds: {string.Join(", ", author.Pseuds)}"),
                                          Markup.FromInterpolated($"Join Date: {author.DateJoined}"),
                                          Markup.FromInterpolated($"ID: {author.Id}"),
                                          Markup.FromInterpolated($"Location: {author.Location}"),
                                          new Rule("[red]Bio[/]"),
                                          new Text(author.Bio)
                                          );


                    grid.AddRow(image, panel);

                    AnsiConsole.Write(grid);
                }


            });
        }
    }
}
