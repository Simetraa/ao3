using ao3.lib.search;

namespace ao3Tests.lib
{
    [TestClass()]
    public class PeopleSearchTests
    {
        [TestMethod()]
        public void GenerateSearchQueryTest()
        {
            var query = new PeopleSearch(null, ["NiallWrites"], []);
            Assert.AreEqual("https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bfandom%5D=&people_search%5Bname%5D=NiallWrites", query.GenerateSearchQuery());
            query = new PeopleSearch("NiallWrites", [], []);
            Assert.AreEqual("https://archiveofourown.org/people/search?people_search%5Bquery%5D=NiallWrites&people_search%5Bfandom%5D=&people_search%5Bname%5D=", query.GenerateSearchQuery());
            query = new PeopleSearch(null, [], ["Fullmetal Alchemist - All Media Types"]);
            Assert.AreEqual("https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bfandom%5D=Fullmetal%20Alchemist%20-%20All%20Media%20Types&people_search%5Bname%5D=", query.GenerateSearchQuery());
            query = new PeopleSearch(null, [], []);
            Assert.AreEqual("https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bfandom%5D=&people_search%5Bname%5D=", query.GenerateSearchQuery());
            query = new PeopleSearch(null, ["NiallWrites"], ["Fullmetal Alchemist: Brotherhood & Manga"]);
            Assert.AreEqual("https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bfandom%5D=Fullmetal%20Alchemist%3A%20Brotherhood%20%26%20Manga&people_search%5Bname%5D=NiallWrites", query.GenerateSearchQuery());

        }
    }
}