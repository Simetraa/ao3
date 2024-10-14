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

    enum SortBy
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

    enum SortOrder
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
        public WorkSearch(string? query, string? title, string? creators, int? minHits, int? maxHits, int? minKudos, int? maxKudos, int? minComments, int? maxComments, int? minBookmarks, int? maxBookmarks)
        {
            //work_search[query]: 
            //work_search[title]: 
            //work_search[creators]: 
            //work_search[revised_at]: 
            //work_search[complete]: 
            //work_search[crossover]: 
            //work_search[single_chapter]: 0
            //work_search[word_count]: 
            //work_search[language_id]: 
            //work_search[fandom_names]: 
            //work_search[rating_ids]: 
            //work_search[character_names]: 
            //work_search[relationship_names]: 
            //work_search[freeform_names]: 
            //work_search[hits]: 
            //work_search[kudos_count]: 
            //work_search[comments_count]: 
            //work_search[bookmarks_count]: 
            //work_search[sort_column]: _score
            //work_search[sort_direction]: desc


            MinHits = minHits;
            MaxHits = maxHits;
            MinKudos = minKudos;
            MaxKudos = maxKudos;
            MinComments = minComments;
            MaxComments = maxComments;
            MinBookmarks = minBookmarks;
            MaxBookmarks = maxBookmarks;
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


        public string GenerateSearchQuery()
        {



            UriBuilder uriBuilder = new UriBuilder("https://archiveofourown.org/works/search?");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query[System.Net.WebUtility.UrlEncode("work_search[query]")] = "hello world";


            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();

        }

        int? MinHits { get; set; }
        int? MaxHits { get; set; }
        int? MinKudos { get; set; }
        int? MaxKudos { get; set; }
        int? MinComments { get; set; }
        int? MaxComments { get; set; }
        int? MinBookmarks { get; set; }
        int? MaxBookmarks { get; set; }
    }


    
 }
