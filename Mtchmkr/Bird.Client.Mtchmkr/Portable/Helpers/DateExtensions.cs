//using System.Drawing;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Bird.Client.Mtchmkr.Helpers
{
    public static class DateExtensions
    {
        public static string OccuranceSuffix(this DateTime date)
        {
            // var now = DateTime.Now;
            return (date.Day % 10 == 1 && date.Day % 100 != 11) ? "st"
            : (date.Day % 10 == 2 && date.Day % 100 != 12) ? "nd"
            : (date.Day % 10 == 3 && date.Day % 100 != 13) ? "rd"
            : "th";
        }
        public static string ActualDayOfWeek(this DateTime date)
        {
            List<string> names = new List<string>(DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames);
            names.Add("Today");
            names.Add("Tomorrow");
            names.Add("Yesterday");
            int dayofweek = (int)date.DayOfWeek;
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime tomorrow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            DateTime yesterday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
            if (date.IsSameDay(today))
                dayofweek = 8;
            if (date.IsSameDay(tomorrow))
                dayofweek = 9;
            if (date.IsSameDay(yesterday))
                dayofweek = 10;
            return names[dayofweek];
        }
        public static bool IsSameDay(this DateTime date , DateTime other)
        {
            if (date == null || other == null) return false;
            return (date.Year == other.Year && date.Month == other.Month && date.Day == other.Day);
        }
    }
}
