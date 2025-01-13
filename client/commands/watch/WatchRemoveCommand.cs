using System.CommandLine;

namespace ao3.client.commands.watch
{
    public class WatchRemoveCommand : Command
    {
        public WatchRemoveCommand() : base("remove", "Remove a work from the watch list")
        {
            var workId = new Argument<int>(
                name: "id",
                description: "Removes a work id from the watch list.");

            AddArgument(workId);

            // include arguments and options

            this.SetHandler((context) =>
            {
                var id = context.ParseResult.GetValueForArgument(workId);

                using var file = new FileStream("watch.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var br = new BinaryReader(file);
                using var bw = new BinaryWriter(file);

                List<(int Id, long Updated, string Title, string Author, int CompletedChapters, int TotalChapters)> watchList = new();

                Console.WriteLine("Reading existing watch list...");

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

                if (!watchList.Any(w => w.Id == id))
                {
                    Console.WriteLine("Work not found in watch list");
                    return;
                }

                // Remove the work from the watch list
                var workIndex = watchList.FindIndex(w => w.Id == id);

                var work = watchList[workIndex];
                Console.WriteLine($"Removing {work.Title} from watch list");

                watchList.RemoveAt(workIndex);

                file.SetLength(0);  // Reset the contents of the file
                file.Seek(0, SeekOrigin.Begin); // Reset the file position to the beginning

                foreach (var (Id, Updated, Title, Author, CompletedChapters, TotalChapters) in watchList)
                {
                    bw.Write(Id);
                    bw.Write(Updated);
                    bw.Write(Title);
                    bw.Write(Author);
                    bw.Write(CompletedChapters);
                    bw.Write(TotalChapters);
                }

                Console.WriteLine("Watch list updated successfully");
            });
        }
    }
}
