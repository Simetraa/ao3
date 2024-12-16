using ao3.lib.search;
using ao3.lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ao3Tests.lib.search
{
    [TestClass()]
    public class WorkSearchTests
    {

        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [TestMethod()]
        public async Task SearchTest()
        {
            var query = new WorkSearch("Pureblood pretense",
                           "",
                           "murkybluematter",
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

            var s = query.GenerateSearchQuery();

            TestContext.WriteLine(s);

            var results = await query.Search();
            Console.WriteLine(results);


            Assert.AreEqual(results.works.Count(), 10);
        }
    }
}