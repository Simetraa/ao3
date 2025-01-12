using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace ao3.client.widgets
{
    public class PageWidget<T>(int currentPage, int totalPages, int totalItems, IEnumerable<T> page)
    {
        public Rule Render()
        {
            var rule = new Rule();
            rule.Justification = Justify.Right;
            rule.Title = $"{totalItems} found | Page {currentPage} of {totalPages} | Showing {page.Count()} items";

            return rule;
        }
    }
}
