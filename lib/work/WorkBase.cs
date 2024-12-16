namespace ao3.lib.work
{
    public abstract class WorkBase(int id, string title, Rating rating, IEnumerable<Warning> archiveWarnings, IEnumerable<Category> categories, List<string> fandoms, List<string> relationships, List<string> characters, bool completed, string description, string authorString, List<string> freeformTags, string language, int words, int completedChapters, int? totalChapters, int kudos, int bookmarks, int hits)
    {


        public int Id { get; } = id;
        public string Title { get; } = title;
        public Rating Rating { get; } = rating;
        public IEnumerable<Warning> ArchiveWarnings { get; } = archiveWarnings;
        public IEnumerable<Category> Categories { get; } = categories;
        public List<string> Fandoms { get; } = fandoms;
        public List<string> Relationships { get; } = relationships;
        public List<string> Characters { get; } = characters;
        public bool Completed { get; } = completed;
        public string Description { get; } = description;
        // can there be multiple authors on a fic?
        public string AuthorString { get; } = authorString;
        public List<string> FreeformTags { get; } = freeformTags;
        public string Language { get; } = language;
        public int Words { get; } = words;
        public int CompletedChapters { get; } = completedChapters;
        public int? TotalChapters { get; } = totalChapters;
        public int Kudos { get; } = kudos;
        public int Bookmarks { get; } = bookmarks;
        public int Hits { get; } = hits;

        public async Task<string> Download(DownloadType downloadType, string outputFormat)
        {
            using var client = new HttpClient();

            string fileType;

            switch (downloadType)
            {
                case DownloadType.PDF:
                    fileType = "pdf";
                    break;
                case DownloadType.EPUB:
                    fileType = "epub";
                    break;
                case DownloadType.MOBI:
                    fileType = "mobi";
                    break;
                default:
                    throw new ArgumentException("Invalid download type");
            }

            using var res = await client.GetAsync($"https://download.archiveofourown.org/downloads/{Id}/fic.{fileType}");

            var fileName = res.Content.Headers.ContentDisposition!.FileName!;
            Console.WriteLine($"Default filename: {fileName}");
            fileName = fileName[1..^1]; // remove quotation marks around filename
            var fileExtension = Path.GetExtension(fileName)[1..]; // remove leading dot
            fileName = outputFormat.Replace("%title%", Title)
                                   .Replace("%author%", AuthorString)
                                   .Replace("%id%", Id.ToString())
                                   .Replace("%language%", Language)
                                   .Replace("%words%", Words.ToString())
                                   .Replace("%ext%", fileExtension);

            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '-');
            }



            Console.WriteLine("Downloading to:" + fileName);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                await res.Content.CopyToAsync(fs);
            }

            return fileName;
        }
    }




}
