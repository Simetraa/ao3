namespace ao3.lib
{
    public class WorkList
    {
        private int CurrentPage;
        private string Works;
        private int? PageCount;
        private int? WorkCount;
        public WorkList(string urlBase, int currentPage = 1)
        {

            // call Update() initally in order to fill in PageCount and WorkCount
        }
    }
}
    //    public static (int PageCount, int WorkCount) ParseAuthorPageMeta(AngleSharp.Dom.IElement document)
    //    {
    //        var heading = document.QuerySelector("h2.heading").TextContent;
    //        var headingRegex = new System.Text.RegularExpressions.Regex("\\d+ - (\\d+) of (\\d+)");
    //        var match = headingRegex.Match(heading);
    //        var pageCount = int.Parse(match.Groups[1].Value);
    //        var workCount = int.Parse(match.Groups[2].Value);

    //        return (pageCount, workCount);
    //    }
    //}
