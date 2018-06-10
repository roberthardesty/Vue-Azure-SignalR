using System;
using System.Diagnostics;
using System.Text;

namespace ipman.shared.Utilities
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns true if the specified DateTime is a leap year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime date) {
            return date.Year % 4 == 0 && (date.Year % 100 != 0 || date.Year % 400 == 0);
        }
        /// <summary>
        /// Return a "humanized" representation of the how long ago the date was (ex. 3 weeks, 4 years, etc.)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToHumanizedString(this DateTime date)
        {
            var delta = DateTime.Now - date;

            Func<int, string> plural = x => x != 1 ? "s" : null;

            var years = delta.Days / 365;
            if (years > 0)
                return String.Format("{0} year{1}", years, plural(years));

            var weeks = delta.Days / 7;
            if (weeks > 0)
                return String.Format("{0} week{1}", weeks, plural(weeks));

            if (delta.Days > 0)
                return String.Format("{0} day{1}", delta.Days, plural(delta.Days));

            if (delta.Hours > 0)
                return String.Format("{0} hour{1}", delta.Hours, plural(delta.Hours));

            if (delta.Minutes > 0)
                return String.Format("{0} minute{1}", delta.Minutes, plural(delta.Minutes));

            return "a few seconds";
        }

        /// <summary>
        /// If the value is null, returns the current date/time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime NowIfNull(this DateTime? date)
        {
            return date.HasValue ? date.Value : DateTime.Now;
        }

        /// <summary>
        /// If the value is null, returns the current UTC date/time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime UtcNowIfNull(this DateTime? date)
        {
            return date.HasValue ? date.Value : DateTime.UtcNow;
        }

        /// <summary>
        /// converts the date into a unix-style timestamp (seconds since epoch)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ulong ToUnixStyleTimestamp(this DateTime date)
        {
            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var ts = date - epochStart;
            return Convert.ToUInt64(ts.TotalSeconds);
        }

        public static DateTime SqLiteToUtc(this DateTime date)
        {
            // Sqlite returns a utc time as CORRECT localtime. 
            return date.ToUniversalTime(); 
        }

        public static DateTime? SqLiteToUtc(this DateTime? date)
        {
            if (!date.HasValue) return null;
            return SqLiteToUtc(date.Value); 
        }

        public static DateTime SqlServerToUtc(this DateTime date)
        {
            // Sqlserver returns it in UTC, but not flagged as a utc date. 
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
            return date; 
        }

        public static DateTime? SqlServerToUtc(this DateTime? date)
        {
            return date?.SqlServerToUtc();
        }

        /// <summary>
        /// Because of persistence precision problems, if two dates are within 3 seconds
        /// will consider them the same.   3 seconds = "network lag worst case" for time check
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool ReasonablyEquals(this DateTime date1, DateTime? date2)
        {
            if (date2 == null) return false;

            return Math.Abs(date1.Subtract(date2.Value).TotalSeconds) <= 3.0; 
        }

        /// <summary>
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToPrettyFormat(this TimeSpan span)
        {
            // borrowed from  http://stackoverflow.com/questions/5438363/timespan-pretty-time-format-in-c-sharp 
            // and modified.

            if (span == TimeSpan.Zero) return "0 minutes";

            var sb = new StringBuilder();
            if (span.Days > 0)
                sb.AppendFormat("{0}d ", span.Days);
            if (span.Hours > 0)
                sb.AppendFormat("{0} hr ", span.Hours);
            if (span.Minutes > 0)
                sb.AppendFormat("{0} min ", span.Minutes);
            if (span.Seconds > 0)
                sb.AppendFormat("{0} sec", span.Seconds);
            return sb.ToString().Trim();

        }

        public static string ToBrowseFormat(this TimeSpan? span)
        {
            try
            {
                if (span > new TimeSpan(1, 0, 0))
                {
                    decimal? hours = span.Value.Hours + Math.Round(span.Value.Minutes / 60m, 2);
                    return $@"{hours} hours";
                }
                else if (span > new TimeSpan(0, 1, 0))
                {
                    decimal? minutes = span.Value.Minutes + Math.Round(span.Value.Seconds / 60m, 1);
                    return $@"{minutes} minutes";
                }
                else if (span.Equals(null) || span < new TimeSpan(0, 0, 1))
                {
                    return @"1 second";
                }
                else
                {
                    return $@"{span:%s} seconds";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
