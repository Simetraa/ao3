using ao3.lib.autocomplete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ao3Tests.lib.autocomplete
{
    [TestClass()]
    public class AuthorAutocompleteTests
    {
        [TestMethod()]
        public async Task AutocompleteAsyncTestAsync()
        {
            var authorList = await AuthorAutocomplete.AutocompleteAsync("erraticmuse");
            CollectionAssert.Contains(authorList.ToList(), "erraticmuse");
            Assert.AreEqual(1, authorList.Count());
        }
    }
}