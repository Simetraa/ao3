using ao3.lib.search;

namespace ao3Tests.lib.search
{
    [TestClass()]
    public class AuthorSearchTests

    {
        [TestMethod()]
        public void GenerateSearchQueryTest()
        {
            var query = new AuthorSearch(null, ["NiallWrites"], []);
            var expected = "https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bname%5D=NiallWrites";
            Assert.AreEqual(expected, query.GenerateSearchQuery());
            query = new AuthorSearch("NiallWrites", [], []);
            expected = "https://archiveofourown.org/people/search?people_search%5Bquery%5D=NiallWrites";
            Assert.AreEqual(expected, query.GenerateSearchQuery());
            query = new AuthorSearch(null, [], ["Fullmetal Alchemist - All Media Types"]);
            expected = "https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bfandom%5D=Fullmetal%20Alchemist%20-%20All%20Media%20Types";
            Assert.AreEqual(expected, query.GenerateSearchQuery());
            query = new AuthorSearch(null, ["NiallWrites"], ["Fullmetal Alchemist: Brotherhood & Manga"]);
            expected = "https://archiveofourown.org/people/search?people_search%5Bquery%5D=&people_search%5Bname%5D=NiallWrites&people_search%5Bfandom%5D=Fullmetal%20Alchemist%3A%20Brotherhood%20%26%20Manga";
            Assert.AreEqual(expected, query.GenerateSearchQuery());
            query = new AuthorSearch(null, [], []);
            expected = "https://archiveofourown.org/people/search?people_search%5Bquery%5D=";
            Assert.AreEqual(expected, query.GenerateSearchQuery());
        }
    }
}