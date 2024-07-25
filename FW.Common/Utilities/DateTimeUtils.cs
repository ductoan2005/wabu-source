using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Utilities
{
    public static class DateTimeUtils
    {
        public static DateTime ConvertStringToDataTime(string datetimeStr, string format)
        {
            DateTime dateTime = DateTime.MinValue;
            DateTime.TryParseExact(datetimeStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            return dateTime;
        }
    }
}
