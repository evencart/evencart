using System;

namespace RoastedMarketplace.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullEmptyOrWhitespace(this string s)
        {
            s = s?.Trim();
            return string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(s);
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