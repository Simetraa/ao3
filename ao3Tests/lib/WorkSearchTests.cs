using ao3.lib;
using ao3.lib.search;

namespace ao3Tests.lib
{
    [TestClass()]
    public class WorkSearchTests
    {
        [TestMethod()]
        public void GenerateSearchQueryTest()
        {
            var query = new WorkSearch("Mr Vimes'd Go Spare",
                           "",
                           "",
                           "",
                           CompletionStatus.All,
                           Crossovers.Include,
                           false,
                           "",
                           [],
                           null,
                           [],
                           [],
                           [],
                           [],
                           [],
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           SortColumn.BestMatch,
                           SortDirection.Descending);
            Assert.AreEqual(query.GenerateSearchQuery(), "https://archiveofourown.org/works/search?work_search%5Bquery%5D=Mr%20Vimes%27d%20Go%20Spare&work_search%5Btitle%5D%5D=&work_search%5Bcreators%5D=&work_search%5Brevised_at%5D%5D=&work_search%5Bcomplete%5D%5D=14&work_search%5Bcrossover%5D%5D=0&work_search%5Bsingle_chapter%5D%5D=F&work_search%5Blanguage_id%5D%5D=&work_search%5Bfandom_names%5D=&work_search%5Brating_ids%5D=&work_search%5Bcharacter_names%5D=&work_search%5Brelationship_names%5D=&work_search%5Bfreeform_names%5D=&work_search%5Bword_count%5D=&work_search%5Bhits%5D=&work_search%5Bkudos_count%5D=&work_search%5Bcomments_count%5D=&work_search%5Bbookmarks_count%5D=&work_search%5Bsort_column%5D=_score&work_search%5Bsort_direction%5D=desc&work_search%5Bcomplete%5D=");
        }
    }
}