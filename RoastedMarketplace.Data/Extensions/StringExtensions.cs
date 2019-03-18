using System;
using System.Text.RegularExpressions;

namespace RoastedMarketplace.Data.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces multiple space occurances with single space in a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CleanUpSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }

        public static string RemoveSpecialChars(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            str = str.Replace(';', ',');
            str = str.Replace('\r', ' ');
            str = str.Replace('\n', ' ');
            str = str.Replace("\t", "   ");
            str = str.Replace("¼", "1/4");
            str = str.Replace("½", "1/2");
            str = str.Replace("¾", "3/4");

            return str;
        }

        public static string StripHtml(this string str)
        {
            var tagPattern = @"<[!--\W*?]*?[/]*?\w+.*?>";

            var matches = Regex.Matches(str, tagPattern);

            foreach (Match match in matches)
            {
                str = str.Replace(match.Value, String.Empty);
            }

            return str;

        }

        public static string EncloseWith(this string str, char encloseCharacter)
        {
            return encloseCharacter + str + encloseCharacter;
        }

        public static string ToTitleCase(this string str)
        {
            string result = str;

            if (!String.IsNullOrEmpty(str))
            {
                str.ToLower();
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = String.Join(" ", words);
            }
            return result;
        }

        public static string ToCamelCase(this string str)
        {
            if (str.IsNullEmptyOrWhiteSpace())
                return str;
            if (str.Length > 1)
                return str[0].ToString().ToLowerInvariant() + str.Substring(1);
            return str.ToLower();
        }
        public static bool IsNullEmptyOrWhiteSpace(this string str)
        {
            str = str?.Trim();
            return String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
        }

        public static string Reverse(this string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ReplaceFirstOccurance(this string s, string search, string replace, StringComparison stringComparison = StringComparison.Ordinal)
        {
            var indexOf = s.IndexOf(search, stringComparison);
            if (indexOf == -1)
                return s;
            return s.Remove(indexOf, search.Length).Insert(indexOf, replace);
        }
    }
}
