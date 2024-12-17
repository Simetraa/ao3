using System.Globalization;

namespace ao3.lib
{
    public class Utils
    {
        public static readonly Dictionary<string, Rating> RatingDict = new()
        {
            ["General Audiences"] = Rating.GeneralAudiences,
            ["Teen And Up Audiences"] = Rating.TeenAndUpAudiences,
            ["Mature"] = Rating.Mature,
            ["Explicit"] = Rating.Explicit,
            ["Not Rated"] = Rating.NotRated
        };

        public static readonly Dictionary<string, Warning> WarningDict = new()
        {
            ["No Archive Warnings Apply"] = Warning.NoArchiveWarningsApply,
            ["Creator Chose Not To Use Archive Warnings"] = Warning.CreatorChoseNotToUseArchiveWarnings,
            ["Choose Not To Use Archive Warnings"] = Warning.CreatorChoseNotToUseArchiveWarnings, // can happen in meta
            ["Graphic Depictions Of Violence"] = Warning.GraphicDepictionsOfViolence,
            ["Major Character Death"] = Warning.MajorCharacterDeath
        };

        public static readonly Dictionary<string, CompletionStatus> CompletionStatusDict = new()
        {
            ["Complete Work"] = CompletionStatus.Complete,
            ["Work in Progress"] = CompletionStatus.InProgress,
            ["All"] = CompletionStatus.All
        };

        public static readonly Dictionary<string, Crossovers> CrossoversDict = new()
        {
            ["Include"] = Crossovers.Include,
            ["Exclude"] = Crossovers.Exclude,
            ["Only"] = Crossovers.Only
        };

        public static readonly Dictionary<string, Category> CategoryDict = new()
        {
            ["F/F"] = Category.FF,
            ["F/M"] = Category.FM,
            ["Gen"] = Category.Gen,
            ["No category"] = Category.Gen, // can happen on meta
            ["M/M"] = Category.MM,
            ["Multi"] = Category.Multi,
            ["Other"] = Category.Other
        };

        public static int? TryParseNullable(string val)
        {
            return int.TryParse(val, out int outValue) ? outValue : null;
        }

        public static DateOnly ParseDate(string date)
        {
            return DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        }

        public static DateOnly ParseMetaDate(string date)
        {
            return DateOnly.ParseExact(date, "dd MMM yyyy", CultureInfo.InvariantCulture);
        }

        public static int ParseNumber(string str)
        {
            return int.Parse(str.Replace(",", ""));
        }


        // Parse a string containing a range into a min and max.
        public static string FormatRange(int? min, int? max)
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

        // Parse a string containing a range into a min and max.
        public static (int?, int?) ParseRange(string value)
        {
            int? minValue;
            int? maxValue;

            if (value.Contains('-'))
            {
                var values = value.Split("-");
                minValue = TryParseNullable(values[0]);
                maxValue = TryParseNullable(values[1]);
            }
            else if (value.Contains('>'))
            {
                minValue = TryParseNullable(value.Replace(">", ""));
                maxValue = null;
            }
            else if (value.Contains('<'))
            {
                minValue = null;
                maxValue = TryParseNullable(value.Replace("<", ""));
            }
            else
            {
                minValue = TryParseNullable(value);
                maxValue = TryParseNullable(value);
            }

            return (minValue, maxValue);
        }
        // we need

        // 1. Method to take in two DateTimes and return a string that uses "x-y days ago" search format accepted by ao3
        // 2. Method to take in an ao3 date string and return (DateTime?, DateTime?)
        // 
        public static (DateTime?, DateTime?) ParseDateString(string value)
        {
            // https://github.com/otwcode/otwarchive/blob/master/app/models/search_range.rb

            throw new NotImplementedException();
        }

        public static string FormatDateRange(DateOnly? min, DateOnly? max, DateOnly? current = null)
        {
            //Examples (taking Wednesday 25th April 2012 as the current day):


            //        7 days ago(this will return all works posted/ updated on Wednesday 18th April)
            //1 week ago(this will return all works posted/ updated in the week starting Monday 16th April and ending Sunday 22nd April)
            //2 months ago(this will return all works posted/ updated in the month of February)
            //3 years ago(this will return all works posted/ updated in 2010)
            //< 7 days(this will return all works posted/ updated within the past seven days)
            //> 8 weeks(this will return all works posted/ updated more than eight weeks ago)
            //13 - 21 months(this will return all works posted/ updated between thirteen and twenty-one months ago)

            // return the number of days between the two dates, or > days, or < days

            //    {
            //        return $">{now.Subtract(max).Days}";
            //    }
            //    else if (max == null)
            //    {
            //        return $"<{current.Subtract(min).Days}";
            //    }
            //    else
            //    {
            //        return $"{current.Subtract(min).Days} - {current.Subtract(max).Days}";
            //    }
            //}


            DateTime now = current?.ToDateTime(TimeOnly.Parse("10:00 PM")) ?? DateTime.Now;

            var maxDateTime = max?.ToDateTime(TimeOnly.Parse("10:00 PM")) ?? null;
            var minDateTime = min?.ToDateTime(TimeOnly.Parse("10:00 PM")) ?? null;

            if (minDateTime == null && maxDateTime != null)
            {
                return $">{now.Subtract(maxDateTime.Value).Days}";
            }
            else if (minDateTime == null && maxDateTime != null)
            {
                return $"<{now.Subtract(maxDateTime.Value).Days}";
            }
            else if (minDateTime != null && maxDateTime != null)
            {
                return $"{now.Subtract(minDateTime.Value).Days} - {now.Subtract(maxDateTime.Value).Days}";
            }
            else
            {
                return "";
            }
        }
    }

}

