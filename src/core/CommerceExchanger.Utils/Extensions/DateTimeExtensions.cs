using System;
using System.Globalization;

namespace CommerceExchanger.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool Parse(string dateTime, string format, out DateTimeOffset date)
        {
            return DateTimeOffset.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date);
        }
    }
}