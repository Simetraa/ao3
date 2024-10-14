using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ao3
{
    public class Utils
    {
        public static int? TryParseNullable(string val)
        {
            return int.TryParse(val, out int outValue) ? (int?)outValue : null;
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

    }
}
