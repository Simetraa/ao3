﻿using AngleSharp;
using AngleSharp.Dom;
using ao3.lib.exceptions;
using ao3.lib.search;
using ao3.lib.work;
using Spectre.Console;

namespace ao3.lib.author
{
    public class Author(string id, string name, string dateJoined, string? location, DateOnly? birthday, IEnumerable<string> pseuds, string? bio, string avatarUrl) : AuthorBase(name)
    {
        public string Id { get; private set; } = id;
        public string AvatarUrl { get; private set; } = avatarUrl;
        public string DateJoined { get; private set; } = dateJoined;
        public string? Location { get; private set; } = location;
        public DateOnly? Birthday { get; private set; } = birthday;
        public IEnumerable<string> Pseuds { get; private set; } = pseuds;
        public string? Bio { get; private set; } = bio;



        public static Author ParseFromAuthor(IDocument document)
        {

            var selector = "dl[class='meta'] > dd:nth-child(4)";
            var cells = document.QuerySelector(selector) ?? throw new AuthorNotFoundException();
            var nameSelector = "dl[class='meta'] > dd:nth-of-type(1)";
            var name = document.QuerySelector(nameSelector)!.TextContent;

            var dateJoinedSelector = "dl[class='meta'] > dd:nth-of-type(2)";
            var dateJoined = document.QuerySelector(dateJoinedSelector)!.TextContent;

            var idSelector = "dl[class='meta'] > dd:nth-of-type(3)";
            var id = document.QuerySelector(idSelector)!.TextContent;

            var pseudsSelector = ".pseud ul li a";
            var pseuds = document.QuerySelectorAll(pseudsSelector).Select(i => i.TextContent);

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
            if(url.StartsWith("/"))
            {
                url = "https://archiveofourown.org/images/skins/iconsets/default/icon_user.png"; // default avatar
            }


            return new Author(id, name, dateJoined, location, birthday, pseuds, bio, url);
        }
        public static async Task<Author> ParseFromName(string name)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"https://archiveofourown.org/users/{name}/profile";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            return ParseFromAuthor(document);
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