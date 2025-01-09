using AngleSharp;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.lib.author
{
    public class Author(string id, string name, string dateJoined, string? location, DateOnly? birthday, string[] pseuds, string? bio, string avatarUrl) : AuthorBase(name)
    {
        public string Id { get; } = id;
        public string AvatarUrl { get; } = avatarUrl;
        public string DateJoined { get; } = dateJoined;
        public string? Location { get; } = location;
        public DateOnly? Birthday { get; } = birthday;

        public string[] Pseuds { get; } = pseuds;

        public string? Bio { get; } = bio;


        public static async Task<Author> ParseAsync(string name)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"https://archiveofourown.org/users/{name}/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            var selector = "dl[class='meta'] > dd:nth-child(4)";
            var cells = document.QuerySelector(selector);

            var nameSelector = "dl[class='meta'] > dd:nth-of-type(1)";
            var _ = document.QuerySelector(nameSelector)!.TextContent;

            var dateJoinedSelector = "dl[class='meta'] > dd:nth-of-type(2)";
            var dateJoined = document.QuerySelector(dateJoinedSelector)!.TextContent;

            var idSelector = "dl[class='meta'] > dd:nth-of-type(3)";
            var id = document.QuerySelector(idSelector)!.TextContent;

            var pseudsSelector = ".pseud ul li a";
            var pseuds = document.QuerySelectorAll(pseudsSelector).Select(i => i.TextContent).ToArray();

            var locationSelector = "dt.location + dd";
            var location = document.QuerySelector(locationSelector)?.TextContent;

            var birthdaySelector = "dt.birthday + dd";
            var birthdayText = document.QuerySelector(birthdaySelector)?.TextContent;
            DateOnly? birthday = birthdayText == null ? null : Utils.ParseDate(birthdayText);


            var bioParagraphsSelector = ".bio blockquote p";
            var bioParagraphEls = document.QuerySelectorAll(bioParagraphsSelector);

            var bioParagraphs = bioParagraphEls
                .Select(p => p.TextContent);
            
            var bio = string.Join("\n", bioParagraphs);


            var avatarSelector = ".icon img";
            var url = document.QuerySelector(avatarSelector)!.GetAttribute("src")!;

            return new Author(id, name, dateJoined, location, birthday, pseuds, bio, url);
        }

        public async Task<(int pageCount, int workCount, IEnumerable<WorkMeta> works)> GetWorks(int page = 1)
        {
            var search = new WorkSearch("", "", Name, "", CompletionStatus.All, Crossovers.Include, false, "", [],
                                        null, [], [], [], [], [], null, null, null, null, null, null, null, null, null,
                                        null, SortColumn.DatePosted, SortDirection.Descending);
            (int pageCount, int workCount, IEnumerable<WorkMeta> works) = await search.Search(page);


            return (pageCount, workCount, works);
        }

        public async Task<Stream> DownloadAvatar()
        {
            using var client = new HttpClient();
            return await client.GetStreamAsync(AvatarUrl);
        }
    }
}