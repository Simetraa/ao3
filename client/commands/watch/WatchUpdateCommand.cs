using System.CommandLine;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.client.commands.watch
{
    public class WatchUpdateCommand : Command
    {
        public WatchUpdateCommand() : base("update", "List and update watched works")
        {
            // include arguments and options

            this.SetHandler(async (context) =>
            {
                using var file = new FileStream("watch.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var br = new BinaryReader(file);
                using var bw = new BinaryWriter(file);

                List<(int Id, long Updated, string Title, string Author, int CompletedChapters, int TotalChapters)> watchList = [];

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

                List<(int Id, long Updated, string Title, string Author, int CompletedChapters, int TotalChapters)> newWatchList = [];

                foreach (var (Id, Updated, Title, Author, CompletedChapters, TotalChapters) in watchList)
                {
                    var work = await Work.ParseFromIdAsync(Id);

                    var newUpdateOrPublish = work.Updated ?? work.Published;
                    var newWorkUpdatedBinary = newUpdateOrPublish.ToDateTime(TimeOnly.MinValue).ToBinary();
                    var newWorkTotalChapters = work.TotalChapters ?? -1;

                    newWatchList.Add((work.Id, newWorkUpdatedBinary, work.Title, work.AuthorString, work.CompletedChapters, newWorkTotalChapters));
                }

                file.SetLength(0);  // Reset the contents of the file
                file.Seek(0, SeekOrigin.Begin); // Reset the file position to the beginning

                foreach (var (Id, Updated, Title, Author, CompletedChapters, TotalChapters) in newWatchList)
                {
                    bw.Write(Id);
                    bw.Write(Updated);
                    bw.Write(Title);
                    bw.Write(Author);
                    bw.Write(CompletedChapters);
                    bw.Write(TotalChapters);
                }


                // print differences

                var table = new Table();
                table.Expand();
                table.Border = TableBorder.Rounded;

                table.AddColumn("Id");
                table.AddColumn("Title");
                table.AddColumn("Author");
                table.AddColumn("Chapters");
                table.AddColumn("Total Chapters");
                table.AddColumn("Last Updated");


                // check if the old and new values are different, if so, format the text
                static Markup formatDiff(string oldText, string newText) => new(oldText == newText ? newText : $"[red strikethrough]{oldText}[/] [green]{newText}[/]");

                foreach (var ((newId, newUpdated, newTitle, newAuthor, newCompletedChapters, newTotalChapters),
                (oldId, oldUpdated, oldTitle, oldAuthor, oldCompletedChapters, oldTotalChapters)) in newWatchList.Zip(watchList))
                {
                    // Add logic to compare and print differences
                    var idText = formatDiff(oldId.ToString(), newId.ToString());
                    var oldUpdatedString = DateTime.FromBinary(oldUpdated).ToString("dd-MM-yyyy");
                    var newUpdatedString = DateTime.FromBinary(newUpdated).ToString("dd-MM-yyyy");
                    var updatedText = formatDiff(oldUpdatedString.ToString(), newUpdatedString.ToString());
                    var titleText = formatDiff(oldTitle, newTitle);
                    var authorText = formatDiff(oldAuthor, newAuthor);
                    var chaptersText = formatDiff(oldCompletedChapters.ToString(), newCompletedChapters.ToString());
                    var totalChaptersText = formatDiff(oldTotalChapters == -1 ? "?" : oldTotalChapters.ToString(), newTotalChapters == -1 ? "?" : newTotalChapters.ToString());
                    table.AddRow(idText, titleText, authorText, chaptersText, totalChaptersText, updatedText);
                }
                AnsiConsole.Write(table);
            });

        }
    }
}
