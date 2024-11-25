using ao3.lib;

namespace ao3Tests
{
    [TestClass()]
    public class AuthorTests
    {
        [TestMethod()]
        public async Task GetWorksTestAsync()
        {
            var author = await Author.ParseAsync("a_procession_of_thoughts");
            (int pageCount, int workCount, IEnumerable<WorkMeta> works) = await author.GetWorks();
            Assert.AreEqual(159, workCount);
            Assert.AreEqual(8, pageCount);
            Assert.AreEqual(20, works.Count());

        }


        [TestMethod()]
        public async Task ParseAsyncTestAsync()
        {
            var author = await Author.ParseAsync("queenoffeysand");
            Assert.AreEqual("21504565", author.Id);
            Assert.AreEqual("queenoffeysand", author.Name);
            Assert.AreEqual("2024-05-15", author.DateJoined);

        }

    }
}
