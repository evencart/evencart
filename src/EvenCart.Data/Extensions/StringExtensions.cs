#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Text.RegularExpressions;

namespace EvenCart.Data.Extensions
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
            return Regex.Replace(str, @"\s+", " ").Trim();
        }

        public static string ToTitleCase(this string str)
        {
            var result = str;

            if (!string.IsNullOrEmpty(str))
            {
                str.ToLower();
                var words = str.Split(' ');
                for (var index = 0; index < words.Length; index++)
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
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
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
