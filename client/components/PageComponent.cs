using Spectre.Console;

namespace ao3.client.widgets
{
    public class PageComponent<T>(int currentPage, int totalPages, int totalItems, IEnumerable<T> page, string searchUrl)
    {
        public Rule Render()
        {

            var rule = new Rule($"[link={searchUrl}]Open in browser[/] | [red]{totalItems:n0}[/] found | Page [red]{currentPage:n0}[/] of [red]{totalPages:n0}[/] | Showing [red]{page.Count():n0}[/] items");
            rule.Justification = Justify.Right;

            return rule;
        }
    }
}
