using System.CommandLine;
using ao3.lib.work;

namespace ao3.client.commands.watch
{
    public class WatchAddCommand : Command
    {
        public WatchAddCommand() : base("add", "Add a work to the watch list")
        {
            var workId = new Argument<int>(
                name: "id",
                description: "Adds a work id to the watch list.");

            AddArgument(workId);

            // include arguments and options

            this.SetHandler(async (context) =>
            {
                var id = context.ParseResult.GetValueForArgument(workId);

                using var file = new FileStream("watch.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var br = new BinaryReader(file);
                using var bw = new BinaryWriter(file);

                List<(int Id, long Updated, string Title, string Author, int CompletedChapters, int TotalChapters)> watchList = new();

                Console.WriteLine("Checking existing watch list");

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    var watchId = br.ReadInt32(); // id
                    if (watchId == id)
                    {
                        Console.WriteLine("Work already in watch list");
                        return;
                    }

                    var watchUpdated = br.ReadInt64(); // updated
                    var watchTitle = br.ReadString(); // title
                    var watchAuthor = br.ReadString(); // author
                    var watchChapters = br.ReadInt32(); // chapters
                    var watchTotalChapters = br.ReadInt32(); // totalChapters
                    watchList.Add((watchId, watchUpdated, watchTitle, watchAuthor, watchChapters, watchTotalChapters));
                }

                Console.WriteLine("Downloading work data");

                var work = await Work.ParseFromIdAsync(id);

                Console.WriteLine($"Adding {work.Title} to watch list");

                var updateOrPublish = work.Updated ?? work.Published;
                var workUpdatedBinary = updateOrPublish.ToDateTime(TimeOnly.MinValue).ToBinary();
                var workTotalChapters = work.TotalChapters ?? -1;

                watchList.Add((work.Id, workUpdatedBinary, work.Title, work.AuthorString, work.CompletedChapters, workTotalChapters));
                
                file.SetLength(0); // Reset the contents of the file
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
