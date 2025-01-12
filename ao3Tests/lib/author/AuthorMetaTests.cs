using Microsoft.VisualStudio.TestTools.UnitTesting;
using ao3.lib.author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp;

namespace ao3Tests.lib.author
{
    [TestClass()]
    public class AuthorMetaTests
    {
        string html = "<li class=\"user pseud picture blurb group\" role=\"article\">\r\n  <div class=\"header module\">\r\n    <h4 class=\"heading\"><a href=\"/users/erraticmuse/pseuds/erraticmuse\">erraticmuse</a>\r\n    </h4>\r\n      <h5 class=\"heading\"><a href=\"/users/erraticmuse/pseuds/erraticmuse/works\">22 works</a>, <a href=\"/users/erraticmuse/pseuds/erraticmuse/bookmarks\">162 bookmarks</a></h5>\r\n    <div class=\"icon\">\r\n      <a href=\"/users/erraticmuse/pseuds/erraticmuse\"><img alt=\"\" class=\"icon\" src=\"https://s3.amazonaws.com/otw-ao3-icons/icons/12907/standard.jpeg?1595955046\"></a>\r\n    </div>\r\n  </div>\r\n    <blockquote class=\"userstuff\">\r\n      <p></p><p>31. Polish. She/Her. I will read anything once. And sometimes twice or more.</p><p></p>\r\n    </blockquote>\r\n</li>";
        [TestMethod()]
        public async Task ParseFromMetaTest()
        {

            IBrowsingContext context = BrowsingContext.New(Configuration.Default);
            IDocument document = await context.OpenAsync(req => req.Content(html));
            IElement authorElement = document.QuerySelector("li.user")!;

            var author = AuthorMeta.ParseFromMeta(authorElement);
            Assert.AreEqual("erraticmuse", author.Name);
            Assert.AreEqual(null, author.Pseud);
            Assert.AreEqual("https://s3.amazonaws.com/otw-ao3-icons/icons/12907/standard.jpeg?1595955046", author.AvatarUrl);
            Assert.AreEqual(162, author.Bookmarks);
            Assert.AreEqual(22, author.Works);
        }
    }
}