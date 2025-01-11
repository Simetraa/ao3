namespace ao3.lib.work
{
    public abstract class WorkBase(int id, string title, Rating rating, IEnumerable<Warning> archiveWarnings, IEnumerable<Category> categories, List<string> fandoms, List<string> relationships, List<string> characters, bool completed, string description, string authorString, List<string> freeformTags, string language, int words, int completedChapters, int? totalChapters, int kudos, int bookmarks, int hits)
    {
        public int Id { get; private set; } = id;
        public string Title { get; private set; } = title;
        public Rating Rating { get; private set; } = rating;
        public IEnumerable<Warning> ArchiveWarnings { get; private set; } = archiveWarnings;
        public IEnumerable<Category> Categories { get; private set;  } = categories;
        public IEnumerable<string> Fandoms { get; private set; } = fandoms;
        public IEnumerable<string> Relationships { get; private set; } = relationships;
        public IEnumerable<string> Characters { get; private set; } = characters;
        public bool Completed { get; private set; } = completed;
        public string Description { get; private set; } = description;
        // can there be multiple authors on a fic?
        public string AuthorString { get; private set; } = authorString;
        public IEnumerable<string> FreeformTags { get; private set; } = freeformTags;
        public string Language { get; private set; } = language;
        public int Words { get; private set; } = words;
        public int CompletedChapters { get; private set; } = completedChapters;
        public int? TotalChapters { get; private set; } = totalChapters;
        public int Kudos { get; private set; } = kudos;
        public int Bookmarks { get; private set;  } = bookmarks;
        public int Hits { get; private set; } = hits;

        public async Task<string> Download(DownloadType downloadType, string outputFormat)
        {
            using var client = new HttpClient();

            string fileType;

            switch (downloadType)
            {
                case DownloadType.AZW3:
                    fileType = "azw3";
                    break;
                case DownloadType.PDF:
                    fileType = "pdf";
                    break;
                case DownloadType.EPUB:
                    fileType = "epub";
                    break;
                case DownloadType.MOBI:
                    fileType = "mobi";
                    break;
                case DownloadType.HTML:
                    fileType = "html";
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
