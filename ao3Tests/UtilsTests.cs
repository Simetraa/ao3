using Microsoft.VisualStudio.TestTools.UnitTesting;
using ao3.lib;

namespace ao3Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void FormatRangeTest()
        {
            Assert.AreEqual(Utils.FormatRange(1, 5), "1-5");
            Assert.AreEqual(Utils.FormatRange(1, 1), "1");
            Assert.AreEqual(Utils.FormatRange(null, 5), "<5");
            Assert.AreEqual(Utils.FormatRange(1, null), ">1");
        }

        [TestMethod()]
        public void ParseRangeTest()
        {
            Assert.AreEqual(Utils.ParseRange("1-5"), (1, 5));
            Assert.AreEqual(Utils.ParseRange("1"), (1, 1));
            Assert.AreEqual(Utils.ParseRange("<5"), (null, 5));
            Assert.AreEqual(Utils.ParseRange(">1"), (1, null));
        }

        [TestMethod()]
        public void FormatDateRangeTest()
        {
            var current = new DateOnly(2012, 4, 25);
            Assert.AreEqual("> 7", Utils.FormatDateRange(new DateOnly(2012, 4, 15), null));
        }
    }
}