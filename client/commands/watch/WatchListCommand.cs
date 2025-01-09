using System.CommandLine;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.watch
{
    public class WatchListCommand : Command
    {
        public WatchListCommand() : base("list", "List watched works")
        {
            // include arguments and options

            this.SetHandler((context) =>
            {
                using var file = new FileStream("watch.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var br = new BinaryReader(file);
                using var bw = new BinaryWriter(file);

                List<(int Id, long Updated, string Title, string Author, int CompletedChapters, int TotalChapters)> watchList = new();

                Console.WriteLine("Loading existing watch list");

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    var watchId = br.ReadInt32(); // id
                    var watchUpdated = br.ReadInt64(); // updated
                    var watchTitle = br.ReadString(); // title
                    var watchAuthor = br.ReadString(); // author
                    var watchChapters = br.ReadInt32(); // chapters
                    var watchTotalChapters = br.ReadInt32(); // totalChapters
                    watchList.Add((watchId, watchUpdated, watchTitle, watchAuthor, watchChapters, watchTotalChapters));
                }

                Console.WriteLine("Downloading work data");

                var table = new Table();
                table.Expand();
                table.Border = TableBorder.Rounded;

                table.AddColumn("Id");
                table.AddColumn("Title");
                table.AddColumn("Author");
                table.AddColumn("Chapters");
                table.AddColumn("Total Chapters");
                table.AddColumn("Last Updated");

                foreach (var (id, updated, title, author, completedChapters, totalChapters) in watchList)
                {
                    // Add logic to compare and print differences
                    var idText = id.ToString();
                    var updatedText = DateTime.FromBinary(updated).ToString("dd-MM-yyyy");
                    var titleText = title;
                    var authorText = author;
                    var chaptersText = completedChapters.ToString();
                    var totalChaptersText = totalChapters == -1 ? "?" : totalChapters.ToString();
                    table.AddRow(idText, titleText, authorText, chaptersText, totalChaptersText, updatedText);
                }
                AnsiConsole.Write(table);
            });

        }
    }
}
