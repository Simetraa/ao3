using System.CommandLine;
using ao3.client.widgets;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.info
{
    public class InfoWorkCommand : Command
    {
        public InfoWorkCommand() : base("work", "Get info about a work")
        {
            var idArgument = new Argument<int>(
                name: "id"
                );


            AddArgument(idArgument);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var id = context.ParseResult.GetValueForArgument(idArgument);
                try
                {
                    var work = await Work.ParseFromIdAsync(id);
                    var workComponent = new WorkComponent(work.ToWorkMeta());

                    AnsiConsole.Write(workComponent.Render());
                } catch (Exception e)
                {
                    AnsiConsole.WriteException(e);
                } 
            }
            );
        }
    }
}
