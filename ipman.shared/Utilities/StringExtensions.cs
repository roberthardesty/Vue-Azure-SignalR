using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ipman.shared.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string is null or empty.  This is a simple wrapper for string.IsNullOrEmpty()
        /// </summary>
        /// <param name="target">String value to check</param>
        /// <returns>True if <paramref name="target"/> is not null or empty</returns>
        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        /// <summary>
        /// Checks if a string is null or white space.  This is a simple wrapper for string.IsNullOrWhiteSpace()
        /// </summary>
        /// <param name="target">String value to check</param>
        /// <returns>True if <paramref name="target"/> is not null or empty</returns>
        public static bool IsNullOrWhiteSpace(this string target)
        {
            return string.IsNullOrWhiteSpace(target);
        }

        /// <summary>
        /// Returns true if the string can be converted to the specified type
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<TValue>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof (TValue));
            return (converter.CanConvertFrom(typeof (string))) && converter.IsValid(value);
        }

        /// <summary>
        /// Converts the string to TValue or, failing that, returns the specified default value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof (TValue));

                if (converter.CanConvertFrom(typeof (string)))
                {
                    return (TValue) converter.ConvertFrom(value);
                }

                converter = TypeDescriptor.GetConverter(typeof (string));

                if (converter.CanConvertTo(typeof (TValue)))
                {
                    return (TValue) converter.ConvertTo(value, typeof (TValue));
                }
            } catch
            {
            }
            return defaultValue;
        }

        /// <summary>
        /// Converts the string to TValue or, failing that, returns the default value of the type
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue As<TValue>(this string value)
        {
            return value.As(default(TValue));
        }

        /// <summary>
        /// Allows for converting a string into a boolean based on specified true/false strings
        /// </summary>
        /// <example>
        /// <code>
        ///     string myString = "Yes";
        ///     bool realValue = myString.ToBool("Yes", "No");
        ///     Log.Instance.LogMessage(realValue);
        /// </code>
        /// </example>
        /// <param name="value"></param>
        /// <param name="trueString"></param>
        /// <param name="falseString"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool ToBool(this string value, string trueString, string falseString, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            return value.Equals(trueString, comparisonType);
        }

        /// <summary>
        /// Converts the string to a byte array using the default encoding.  This is a simple wrapper for System.Text.Encoding.Default.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string data)
        {
            return System.Text.Encoding.Default.GetBytes(data);
        }

        /// <summary>
        /// Converts the string to a byte array using ASCII encoding.  This is a simple wrapper for System.Text.Encoding.ASCII.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytesAscii(this string data)
        {
            return System.Text.Encoding.ASCII.GetBytes(data);
        }

        /// <summary>
        /// Converts the string to a byte array using UTF8 encoding.  This is a simple wrapper for System.Text.Encoding.Utf8.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytesUtf8(this string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// Converts the string to a byte array using UTF7 encoding.  This is a simple wrapper for System.Text.Encoding.Utf7.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytesUtf7(this string data)
        {
            return System.Text.Encoding.UTF7.GetBytes(data);
        }

        /// <summary>
        /// Converts the string to a byte array using Utf32 encoding.  This is a simple wrapper for System.Text.Encoding.Utf32.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytesUtf32(this string data)
        {
            return System.Text.Encoding.UTF32.GetBytes(data);
        }

        /// <summary>
        /// Converts the string to a byte array using Unicode encoding.  This is a simple wrapper for System.Text.Encoding.Unicode.GetBytes()
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytesUnicode(this string data)
        {
            return System.Text.Encoding.Unicode.GetBytes(data);
        }

        /// <summary>
        /// trims a given string off the end of this string, optionally only trimming first occurrence
        /// </summary>
        /// <param name="value"></param>
        /// <param name="suffix"></param>
        /// <param name="trimAll"></param>
        /// <returns></returns>
        public static string TrimStringFromEnd(this string value, string suffix, bool trimAll = true)
        {
            while (!value.IsNullOrWhiteSpace() && !suffix.IsNullOrWhiteSpace() && value.EndsWith(suffix))
            {
                value = value.Substring(0, value.Length - suffix.Length);
                //if not trimming all, return after the first one
                if (!trimAll)
                {
                    return value;
                }
            }
            return value;
        }

        /// <summary>
        /// converts a string in the version format to a sortable string
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        /// <exception cref="FormatException">thrown in string format doesn't match version formatting</exception>
        public static string ConvertVersionToSortableString(this string version)
        {
            //first make sure the version string actually matches what a version should look like
            //at most, this should be #.#.#.#, and we'll fill in missing on the end if necessary
            var match = Regex.Match(version, @"^(?'major'\d+)\.(?'minor'\d+)(\.(?'build'\d+)){0,1}(\.(?'revision'\d+)){0,1}$", RegexOptions.None);

            if (!match.Success)
                throw new FormatException($"Version string {version} not in correct format");

            //for each part, left pad it with zero up to 6 characters, then recombine all the parts
            //if any part is missing, just fill it with zero
            var major = match.Groups["major"].Value.PadLeft(6, '0');
            var minor = match.Groups["minor"].Value.PadLeft(6, '0');
            var build = (match.Groups["build"].Success ? match.Groups["build"].Value : string.Empty).PadLeft(6, '0');
            var revision = (match.Groups["revision"].Success ? match.Groups["revision"].Value : string.Empty).PadLeft(6, '0');

            return $"{major}.{minor}.{build}.{revision}";
        }
    }
}
