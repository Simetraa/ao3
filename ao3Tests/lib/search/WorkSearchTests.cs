using ao3.lib.search;

namespace ao3Tests.lib.search
{
    [TestClass()]
    public class WorkSearchTests
    {
        [TestMethod()]
        public void GenerateSearchQueryTest()
        {
            var query = new WorkSearch("Seventh Horcrux");
            var expected = "https://archiveofourown.org/works/search?work_search%5Bquery%5D=Seventh%20Horcrux&work_search%5Btitle%5D=&work_search%5Bcreators%5D=&work_search%5Brevised_at%5D=&work_search%5Bcomplete%5D=&work_search%5Bcrossover%5D=&work_search%5Bsingle_chapter%5D=0&work_search%5Bword_count%5D=&work_search%5Blanguage_id%5D=&work_search%5Bfandom_names%5D=&work_search%5Brating_ids%5D=&work_search%5Bcharacter_names%5D=&work_search%5Brelationship_names%5D=&work_search%5Bfreeform_names%5D=&work_search%5Bhits%5D=&work_search%5Bkudos_count%5D=&work_search%5Bcomments_count%5D=&work_search%5Bbookmarks_count%5D=&work_search%5Bsort_column%5D=_score&work_search%5Bsort_direction%5D=desc";
            Assert.AreEqual(expected, query.GenerateSearchQuery());

        }

        [TestMethod()]
        public async Task SearchTest()
        {
            var query = new WorkSearch("Seventh Horcrux");
            var (pageCount, workCount, works) = await query.Search();
            Assert.AreEqual(8, pageCount);
            Assert.AreEqual(works.ElementAt(0).Title, "Seventh Horcrux");
        }
    }
}