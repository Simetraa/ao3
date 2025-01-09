//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ao3.lib.work;

//namespace ao3.client.watch
//{
//    public class WatchList
//    {
//        public void Add(int id)
//        {
//            var file = File.Open("watch.dat", FileMode.OpenOrCreate);
//            // read through the file and check if the id is already in the file
//            var br = new BinaryReader(file);
//            // https://stackoverflow.com/a/63521508
//            var watchList = new List<int> { };
//            while (br.BaseStream.Position < br.BaseStream.Length)
//            {
//                var watchId = br.ReadInt32();
//                watchList.Add(watchId);
//            }
//            if (watchList.Contains(id))
//            {
//                Console.WriteLine("Work already in watch list");
//                return;
//            }

//            br.Close(); // TODO: move these to using
//            Console.WriteLine("Downloading work data");
//            var work = await Work.ParseFromIdAsync(id);
//            Console.WriteLine($"Adding {work.Title} to watch list");
//            var bw = new BinaryWriter(file);
//            for ()
//        }
//    }
//}
