using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using System.Xml.Linq;
using AngleSharp.Dom;
using Microsoft.AspNetCore.WebUtilities;
using static System.Formats.Asn1.AsnWriter;

namespace ao3.lib
{
    public class WorkSearch
    {
        public WorkSearch(string query,
                          string title,
                          string creators,
                          string date,
                          CompletionStatus complete,
                          Crossovers crossovers,
                          bool onlySingleChapter,
                          string language,
                          List<string> fandoms,
                          Rating? ratings,
                          List<Warning> warnings,
                          List<Category> categories,
                          List<string> characters,
                          List<string> relationships,
                          List<string> additionalTags,
                          int? minWords,
                          int? maxWords,
                          int? minHits,
                          int? maxHits,
                          int? minKudos,
                          int? maxKudos,
                          int? minComments,
                          int? maxComments,
                          int? minBookmarks,
                          int? maxBookmarks,
                          SortColumn sortBy,
                          SortDirection sortOrder
            )
        {
            Query = query;
            Title = title;
            Creators = creators;
            Date = date;
            Complete = complete;
            Crossovers = crossovers;
            OnlySingleChapter = onlySingleChapter;
            Language = language;
            Fandoms = fandoms;
            Rating = ratings;
            Warnings = warnings;
            Categories = categories;
            Characters = characters;
            Relationships = relationships;
            AdditionalTags = additionalTags;
            MinWords = minWords;
            MaxWords = maxWords;
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






        public string GenerateSearchQuery()
        {
            string url = "https://archiveofourown.org/works/search";

            url = QueryHelpers.AddQueryString(url, "work_search[query]", Query ?? "");
            url = QueryHelpers.AddQueryString(url, "work_search[title]]", Title);
            url = QueryHelpers.AddQueryString(url, "work_search[creators]", Creators);
            url = QueryHelpers.AddQueryString(url, "work_search[revised_at]]", Date);
            url = QueryHelpers.AddQueryString(url, "work_search[complete]]", Convert.ToString((int)Complete));
            url = QueryHelpers.AddQueryString(url, "work_search[crossover]]", Convert.ToString((int)Crossovers));
            url = QueryHelpers.AddQueryString(url, "work_search[single_chapter]]", OnlySingleChapter ? "T" : "F");
            url = QueryHelpers.AddQueryString(url, "work_search[language_id]]", Language);
            url = QueryHelpers.AddQueryString(url, "work_search[fandom_names]", string.Join(",", Fandoms));
            foreach (var warning in Warnings)
            {
                url = QueryHelpers.AddQueryString(url, "work_search[archive_warning_ids][]", Convert.ToString((int)warning));
            }


            foreach (var category in Categories)
            {
                url = QueryHelpers.AddQueryString(url, "work_search[category_ids][]", Convert.ToString((int)category));
            }

            if (Rating.HasValue) // find a better way than this
            {
                url = QueryHelpers.AddQueryString(url, "work_search[rating_ids]", Convert.ToString((int)Rating));
            }
            else
            {
                url = QueryHelpers.AddQueryString(url, "work_search[rating_ids]", ""); // TODO: This is really messy.

            }

            url = QueryHelpers.AddQueryString(url, "work_search[character_names]", string.Join(",", Characters));
            url = QueryHelpers.AddQueryString(url, "work_search[relationship_names]", string.Join(",", Relationships));
            url = QueryHelpers.AddQueryString(url, "work_search[freeform_names]", string.Join(",", AdditionalTags));
            url = QueryHelpers.AddQueryString(url, "work_search[word_count]", Utils.FormatRange(MinWords, MaxWords));
            url = QueryHelpers.AddQueryString(url, "work_search[hits]", Utils.FormatRange(MinHits, MaxHits));
            url = QueryHelpers.AddQueryString(url, "work_search[kudos_count]", Utils.FormatRange(MinKudos, MaxKudos));
            url = QueryHelpers.AddQueryString(url, "work_search[comments_count]", Utils.FormatRange(MinComments, MaxComments));
            url = QueryHelpers.AddQueryString(url, "work_search[bookmarks_count]", Utils.FormatRange(MinBookmarks, MaxBookmarks));

            switch (SortBy)
            {
                case SortColumn.BestMatch:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "_score");
                    break;
                case SortColumn.Author:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "authors_to_sort_on");
                    break;
                case SortColumn.Title:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "title_to_sort_on");
                    break;
                case SortColumn.DatePosted:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "created_at");
                    break;
                case SortColumn.DateUpdated:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "revised_at");
                    break;
                case SortColumn.WordCount:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "word_count");
                    break;
                case SortColumn.Hits:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "hits");
                    break;
                case SortColumn.Kudos:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "kudos_count");
                    break;
                case SortColumn.Comments:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "comments_count");
                    break;
                case SortColumn.Bookmarks:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_column]", "bookmarks_count");
                    break;
            }

            switch (SortOrder)
            {
                case SortDirection.Descending:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_direction]", "desc");
                    break;
                case SortDirection.Ascending:
                    url = QueryHelpers.AddQueryString(url, "work_search[sort_direction]", "asc");
                    break;
            }

            switch (Complete)
            {
                case CompletionStatus.All:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "");
                    break; // we may be able to remove this
                case CompletionStatus.Complete:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "T");
                    break;
                case CompletionStatus.InProgress:
                    url = QueryHelpers.AddQueryString(url, "work_search[complete]", "F");
                    break;
            }


            //        commit: Search -
            //work_search[query]: -
            //work_search[title]:  - 
            //work_search[creators]: s - 
            //work_search[revised_at]:  - 
            //work_search[complete]: - 
            //work_search[crossover]: -
            //work_search[single_chapter]: 0 - 
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

            return url;
        }

        public static (int pageCount, int workCount) ParseWorkPageMeta(IDocument document)
        {
            var heading = document.QuerySelector("h3.heading")!.TextContent;
            var headingRegex = new System.Text.RegularExpressions.Regex("([\\d,]+) Found");
            var match = headingRegex.Match(heading);
            var workCount = int.Parse(match.Groups[1].Value);

            var pageCountEl = document.QuerySelector(".pagination li:has(+ .next) > a")!.TextContent;
            var pageCount = int.Parse(pageCountEl);

            return (pageCount, workCount);
        }

        public async Task<(int pageCount, int workCount, IEnumerable<WorkMeta> works)> Search(int page = 1)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = GenerateSearchQuery();
            var context = BrowsingContext.New(config);

            address = QueryHelpers.AddQueryString(address, "page", page.ToString());


            var document = await context.OpenAsync(address);

            var workElements = document.QuerySelectorAll("li.work");

            var (pageCount, workCount) = ParseWorkPageMeta(document);

            IEnumerable<WorkMeta> works = workElements.Select(WorkMeta.ParseFromMeta);

            return (pageCount, workCount, works);
        }



        string Query { get; set; }
        string Title { get; set; }
        string? Creators { get; set; }
        string Date { get; set; }
        CompletionStatus Complete { get; set; }
        Crossovers Crossovers { get; set; }
        bool OnlySingleChapter { get; set; }
        string Language { get; set; }
        List<string> Fandoms { get; set; }
        Rating? Rating { get; set; }
        List<Warning> Warnings { get; set; }
        List<Category> Categories { get; set; }
        List<string> Characters { get; set; }
        List<string> Relationships { get; set; }
        List<string> AdditionalTags { get; set; }

        int? MinWords { get; set; }
        int? MaxWords { get; set; }
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
