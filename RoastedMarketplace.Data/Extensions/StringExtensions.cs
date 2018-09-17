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
                str = str.Replace(match.Value, string.Empty);
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

            if (!string.IsNullOrEmpty(str))
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
                result = string.Join(" ", words);
            }
            return result;
        }

        public static bool IsNullEmptyOrWhiteSpace(this string str)
        {
            str = str.Trim();
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
    }
}
