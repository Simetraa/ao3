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
        public static DateOnly ParseDate(string date)
        {
            return DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        }
    }
}
