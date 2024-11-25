using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ao3.commands
{
    public class SearchCommand : Command<SearchCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[Search]")]
            public string Search { get; set; }
            [CommandOption("--query <NAME>")]
            public string Query { get; set; }
            [CommandOption("--title <TITLE>")]
            public string Title { get; set; }



            [CommandOption("--fandom <FANDOM>")]
            public string Fandom { get; set; }

        }


        public override int Execute(CommandContext context, Settings settings)
        {
            AnsiConsole.MarkupLine($"Hello, [blue]{settings.Url}[/]");
            return 0;
        }
    }
}
