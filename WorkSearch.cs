using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Formats.Asn1.AsnWriter;

namespace ao3
{

    enum CompletionStatus
    {
        All,
        Complete,
        InProgress,
    }

    enum Crossovers
    {
        Include,
        Exclude,
        Only,
    }

    enum SortColumn
    {
        BestMatch,
        Author,
        Title,
        DatePosted,
        DateUpdated,
        WordCount,
        Hits,
        Kudos,
        Comments,
        Bookmarks,
    }

    enum SortDirection
    {
        Descending,
        Ascending,
    }

    enum Rating
    {
        NotRated,
        GeneralAudiences,
        TeenAndUpAudiences,
        Mature,
        Explicit,
    }

    public class WorkSearch
    {
        public WorkSearch(string? query, string? title, string? creators, string? date, List<CompletionStatus> complete, List<Crossovers> crossovers, bool isSingleChapter, string wordCount, string language, string fandoms, List<Rating> ratings, List<string> characters, List<string> relationships, List<string> additionalTags, int? minHits, int? maxHits, int? minKudos, int? maxKudos, int? minComments, int? maxComments, int? minBookmarks, int? maxBookmarks, SortColumn sortBy, SortDirection sortOrder)
        {
            Query = query;
            Title = title;
            Creators = creators;
            Date = date;
            Complete = complete;
            Crossovers = crossovers;
            IsSingleChapter = isSingleChapter;
            WordCount = wordCount;
            Language = language;
            Fandoms = fandoms;
            Ratings = ratings;
            Characters = characters;
            Relationships = relationships;
            AdditionalTags = additionalTags;
            MinHits = minHits;
            MaxHits = maxHits;
            MinKudos = minKudos;
            MaxKudos = maxKudos;
            MinComments = minComments;
            MaxComments = maxComments;
            MinBookmarks = minBookmarks;
            MaxBookmarks = maxBookmarks;
            SortBy = sortBy;
            SortOrder = sortOrder;
        }

        // Parse a string containing a range into a min and max.
        static Tuple<int?, int?> ParseRange(string value)
        {
            int? minValue;
            int? maxValue;

            if(value.Contains('-'))
            {
                var values = value.Split("-");
                minValue = Utils.TryParseNullable(values[0]);
                maxValue = Utils.TryParseNullable(values[1]);
            }
            else if (value.Contains('>'))
            {
                minValue = Utils.TryParseNullable(value.Replace(">", ""));
                maxValue = null;
            }
            else if (value.Contains('<'))
            {
                minValue = null;
                maxValue = Utils.TryParseNullable(value.Replace("<", ""));
            }
            else
            {
                minValue = Utils.TryParseNullable(value);
                maxValue = Utils.TryParseNullable(value);
            }

            return new Tuple<int?, int?>(minValue, maxValue);
        }

        // Parse a string containing a range into a min and max.
        static string FormatRange(int? min, int? max)
        {
            // format an optional min and max into an ao3 spec range string

            if (min == max)
            {
                return min.ToString();
            }
            else if (min == null)
            {
                return $"<{max}";
            }
            else if (max == null)
            {
                return $">{min}";
            }
            else
            {
                return $"{min}-{max}";
            }
        }


        public string GenerateSearchQuery()
        {



            UriBuilder uriBuilder = new UriBuilder("https://archiveofourown.org/works/search?");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query[System.Net.WebUtility.UrlEncode("work_search[query]")] = "hello world";


            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();

        }


        string? Query { get; set; }
        string? Title { get; set; }
        string? Creators { get; set; }
        string? Date { get; set; }
        List<CompletionStatus> Complete { get; set; }
        List<Crossovers> Crossovers { get; set; }
        bool IsSingleChapter { get; set; }
        string WordCount { get; set; }
        string Language { get; set; }
        string Fandoms { get; set; }
        List<Rating> Ratings { get; set; }
        List<string> Characters { get; set; }
        List<string> Relationships { get; set; }
        List<string> AdditionalTags { get; set; }
        int? MinHits { get; set; }
        int? MaxHits { get; set; }
        int? MinKudos { get; set; }
        int? MaxKudos { get; set; }
        int? MinComments { get; set; }
        int? MaxComments { get; set; }
        int? MinBookmarks { get; set; }
        int? MaxBookmarks { get; set; }
        SortColumn SortBy { get; set; }
        SortDirection SortOrder { get; set; }




    }


    
 }
