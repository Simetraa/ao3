using AngleSharp.Dom;

namespace ao3.lib.author
{
    public class AuthorMeta(string name, string? pseud, string? avatarUrl, int bookmarks, int works) : AuthorBase(name)
    {
        public string? Pseud { get; private set; } = pseud;
        public string? AvatarUrl { get; private set; } = avatarUrl;
        public int Bookmarks { get; private set; } = bookmarks;

        public int Works { get; private set; } = works;

        public static AuthorMeta ParseFromMeta(IElement html)
        {
            var nameDivSelector = ".user h4 a";
            var name = html.QuerySelector(nameDivSelector)!.TextContent;


            name = name.Replace("\n", "");
            var parts = name.Split(" (");

            string? pseud = null;
            string username = name;

            if (parts.Length == 2)
            {
                pseud = parts[0].Trim();
                username = parts[1].Replace(")", "").Trim();
            }

            var avatarSelector = ".user img";
            var url = html.QuerySelector(avatarSelector)?.GetAttribute("src");

            var bookmarksAndWorksSelector = ".user h5";
            var bookmarksAndWorksString = html.QuerySelector(bookmarksAndWorksSelector)?.TextContent ?? "0 works, 0 bookmarks";

            parts = bookmarksAndWorksString.Split(",");

            int works = 0;
            int bookmarks = 0;

            foreach (var part in parts)
            {
                if (part.Contains("works"))
                {
                    works = Utils.ParseNumber(part.Replace("works", ""));
                }
                else if (part.Contains("bookmarks"))
                {
                    bookmarks = Utils.ParseNumber(part.Replace("bookmarks", ""));
                }
            }

            return new AuthorMeta(username, pseud, url, bookmarks, works);
        }
    }
}
