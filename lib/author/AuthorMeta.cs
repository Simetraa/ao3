using AngleSharp.Dom;

namespace ao3.lib.author
{
    public class AuthorMeta(string name, string avatarUrl) : AuthorBase(name)
    {
        string AvatarUrl { get; } = avatarUrl;

        public static AuthorMeta ParseFromMeta(IElement html, string? avatarUrl)
        {
            var nameSelector = ".user h4 a";
            var name = html.QuerySelector(nameSelector)!.TextContent;

            var avatarSelector = ".user img";
            var url = html.QuerySelector(avatarSelector)!.GetAttribute("src");
            return new AuthorMeta(name, url);
        }
    }

}
