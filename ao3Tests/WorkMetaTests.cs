using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp;
using ao3.lib;

namespace ao3Tests
{
    [TestClass()]
    public class WorkMetaTests
    {
        [TestMethod()]
        public async Task ParseFromMetaTest()
        {
            IBrowsingContext context = BrowsingContext.New(Configuration.Default);
            IDocument document = await context.OpenAsync(req => req.Content("<li id=\"work_32265931\" class=\"work blurb group work-32265931 user-5788483\" role=\"article\">\r\n  \r\n\r\n  <!--title, author, fandom-->\r\n  <div class=\"header module\">\r\n\r\n    <h4 class=\"heading\">\r\n      <a href=\"/works/32265931\">After School</a>\r\n      by\r\n        \r\n      <!-- do not cache -->\r\n      <a rel=\"author\" href=\"/users/NiallWrites/pseuds/NiallWrites\">NiallWrites</a>\r\n\r\n\r\n\r\n      \r\n      \r\n    </h4>\r\n\r\n    <h5 class=\"fandoms heading\">\r\n      <span class=\"landmark\">Fandoms:</span>\r\n      <a class=\"tag\" href=\"/tags/Fullmetal%20Alchemist:%20Brotherhood%20*a*%20Manga/works\">Fullmetal Alchemist: Brotherhood &amp; Manga</a>\r\n      &nbsp;\r\n    </h5>\r\n\r\n    <!--required tags-->\r\n    <ul class=\"required-tags\">\r\n<li> <a class=\"help symbol question modal modal-attached\" title=\"Symbols key\" href=\"/help/symbols-key.html\" aria-controls=\"modal\"><span class=\"rating-general-audience rating\" title=\"General Audiences\"><span class=\"text\">General Audiences</span></span></a></li>\r\n<li> <a class=\"help symbol question modal modal-attached\" title=\"Symbols key\" href=\"/help/symbols-key.html\" aria-controls=\"modal\"><span class=\"warning-no warnings\" title=\"No Archive Warnings Apply\"><span class=\"text\">No Archive Warnings Apply</span></span></a></li>\r\n<li> <a class=\"help symbol question modal modal-attached\" title=\"Symbols key\" href=\"/help/symbols-key.html\" aria-controls=\"modal\"><span class=\"category-het category\" title=\"F/M\"><span class=\"text\">F/M</span></span></a></li>\r\n<li> <a class=\"help symbol question modal modal-attached\" title=\"Symbols key\" href=\"/help/symbols-key.html\" aria-controls=\"modal\"><span class=\"complete-yes iswip\" title=\"Complete Work\"><span class=\"text\">Complete Work</span></span></a></li>\r\n</ul>\r\n    <p class=\"datetime\">29 Jun 2021</p>\r\n  </div>\r\n\r\n  <!--warnings again, cast, freeform tags-->\r\n  <h6 class=\"landmark heading\">Tags</h6>\r\n  <ul class=\"tags commas\">\r\n    <li class=\"warnings\"><strong><a class=\"tag\" href=\"/tags/No%20Archive%20Warnings%20Apply/works\">No Archive Warnings Apply</a></strong></li><li class=\"relationships\"><a class=\"tag\" href=\"/tags/Lan%20Fan*s*Ling%20Yao/works\">Lan Fan/Ling Yao</a></li><li class=\"characters\"><a class=\"tag\" href=\"/tags/Lan%20Fan%20(Fullmetal%20Alchemist)/works\">Lan Fan (Fullmetal Alchemist)</a></li> <li class=\"characters\"><a class=\"tag\" href=\"/tags/Ling%20Yao/works\">Ling Yao</a></li><li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Alternate%20Universe%20-%20Modern%20Setting/works\">Alternate Universe - Modern Setting</a></li> <li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Alternate%20Universe%20-%20High%20School/works\">Alternate Universe - High School</a></li> <li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Short%20One%20Shot/works\">Short One Shot</a></li> <li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Short%20*a*%20Sweet/works\">Short &amp; Sweet</a></li> <li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Drabble/works\">Drabble</a></li> <li class=\"freeforms\"><a class=\"tag\" href=\"/tags/Fluff/works\">Fluff</a></li>\r\n  </ul>\r\n\r\n  <!--summary-->\r\n    <h6 class=\"landmark heading\">Summary</h6>\r\n    <blockquote class=\"userstuff summary\">\r\n      <p>A short drabble of some modern high school AU Lingfan (how original amirite)</p>\r\n    </blockquote>\r\n\r\n\r\n  <!--stats-->\r\n\r\n  <dl class=\"stats\">\r\n      <dt class=\"language\">Language:</dt>\r\n      <dd class=\"language\" lang=\"en\">English</dd>\r\n    <dt class=\"words\">Words:</dt>\r\n    <dd class=\"words\">466</dd>\r\n    <dt class=\"chapters\">CompletedChapters:</dt>\r\n    <dd class=\"chapters\">1/1</dd>\r\n\r\n\r\n\r\n    <dt class=\"comments\">Comments:</dt>\r\n    <dd class=\"comments\"><a href=\"/works/32265931?show_comments=true#comments\">4</a></dd>\r\n\r\n    <dt class=\"kudos\">Kudos:</dt>\r\n    <dd class=\"kudos\"><a href=\"/works/32265931#kudos\">26</a></dd>\r\n\r\n\r\n  <dt class=\"hits\">Hits:</dt>\r\n  <dd class=\"hits\">288</dd>\r\n\r\n  </dl>\r\n\r\n\r\n</li>"));

            IElement workElement = document.QuerySelector("li.work")!;

            var work = Work.ParseFromMeta(workElement);
            Assert.AreEqual("NiallWrites", work.AuthorString);
            //Assert.AreEqual(new DateOnly(2021, 6, 29), work.Updated);
            //Assert.AreEqual(null, work.Published);
            CollectionAssert.AreEqual(new List<string> { "Fullmetal Alchemist: Brotherhood & Manga" }, work.Fandoms);
            CollectionAssert.AreEqual(new List<string> { "Lan Fan/Ling Yao" }, work.Relationships);
            CollectionAssert.AreEqual(new List<string> { "Lan Fan (Fullmetal Alchemist)", "Ling Yao", }, work.Characters);
            CollectionAssert.AreEqual(new List<string>{
                "Alternate Universe - Modern Setting",
                "Alternate Universe - High School",
                "Short One Shot",
                "Short & Sweet",
                "Drabble",
                "Fluff" }, work.FreeformTags);
            Assert.AreEqual(work.Id, 32265931);
            Assert.AreEqual("A short drabble of some modern high school AU Lingfan (how original amirite)", work.Description);
            Assert.AreEqual("After School", work.Title);
            Assert.AreEqual("English", work.Language);
            Assert.AreEqual(466, work.Words);
            Assert.AreEqual(1, work.CompletedChapters);
            Assert.AreEqual(1, work.TotalChapters);
            Assert.AreEqual(26, work.Kudos);
            Assert.AreEqual(288, work.Hits);
            Assert.AreEqual(Rating.GeneralAudiences, work.Rating);
            Assert.AreEqual(Warning.NoArchiveWarningsApply, work.ArchiveWarning);
            Assert.AreEqual(Category.FM, work.Category);
            Assert.AreEqual(true, work.Completed);
        }
    }
}