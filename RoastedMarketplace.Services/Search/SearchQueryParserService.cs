using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RoastedMarketplace.Data.Extensions;


namespace RoastedMarketplace.Services.Search
{
    public class SearchQueryParserService : ISearchQueryParserService
    {

        private Dictionary<string, IList<string>> parameters = null;
        private string searchText = string.Empty;

        private void TryParseValues(string searchQuery)
        {
            //our search pattern may be like status:"open,pending" priority:"1,2" text-to-search

            const string pattern = "([a-zA-Z0-9_-]+):(?:\"(?<v>[^\"]+)\"|(?<v>[^\\s]+))";

            parameters = new Dictionary<string, IList<string>>(StringComparer.InvariantCultureIgnoreCase);
            if (searchQuery.IsNullEmptyOrWhiteSpace())
                return;

            //find the matches in input text
            var matches = Regex.Matches(searchQuery, pattern);
            foreach (Match match in matches)
            {
                //first contains the key {status} and second contains the value {open, pending}
                var key = match.Groups[1].Value.Replace("-", " ");
                var value = match.Groups["v"].Value;

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                    continue; //its quite possible that user has put a space before/after : sign, it's then an invalid filter because value is ' '
                
                //add if doesn't exist
                if (!parameters.ContainsKey(key))
                    parameters.Add(key, new List<string>());

                if (value.Contains(",")) //multiple values are separated by ,
                {
                    //split the values
                    var values = value.Split(',');
                    parameters[key] = parameters[key].Concat(values).ToList();
                }
                else
                {
                    //single value, add directly
                    parameters[key].Add(value);
                }

            }

            //set search text
            searchText = Regex.Replace(searchQuery, pattern, "");
        }

        public int[] ParseToIntegers(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);

            if (!parameters.ContainsKey(parameterName))
                return null;

            var values = parameters[parameterName].ToList();
            return values.Where(x =>
            {
                //remove unnecessary entries
                int intValue;
                return int.TryParse(x, out intValue);
            }).Select(int.Parse).ToArray();
        }

        public string[] ParseToStrings(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);

            if (!parameters.ContainsKey(parameterName))
                return null;

            return parameters[parameterName].Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        public string ParseSearchText(string searchQuery)
        {
            TryParseValues(searchQuery);
            //return the search text
            return searchText.Trim();
        }

        public string ParseString(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);

            return !parameters.ContainsKey(parameterName) ? string.Empty : parameters[parameterName].FirstOrDefault();
        }

        public decimal? ParseDecimal(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);
            if (!parameters.ContainsKey(parameterName))
                return null;
            if (decimal.TryParse(parameters[parameterName].FirstOrDefault(), out decimal output))
            {
                return output;
            }
            return null;
        }

        public DateTime? ParseDateTime(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);
            if (!parameters.ContainsKey(parameterName))
                return null;

            var parsableDateValues = new[] { "today", "tomorrow", "yesterday", "thisweek", "nextweek", "overdue" };
            return parameters[parameterName].Where(x =>
            {
                DateTime outValue;
                return DateTime.TryParse(x, out outValue) || parsableDateValues.Contains(x);
            }).Select(x =>
            {
                if (parsableDateValues.Contains(x))
                {
                    var now = DateTime.UtcNow;
                    switch (x)
                    {
                        case "overdue":
                        case "today":
                            return now;
                        case "tomorrow":
                            return now.AddDays(1);
                        case "yesterday":
                            return now.AddDays(-1);
                        case "thisweek":
                            var thisWeekDiffDays = now.DayOfWeek == DayOfWeek.Sunday ? 0 : 7 - (int) now.DayOfWeek;
                            return now.AddDays(thisWeekDiffDays);
                        case "nextweek":
                            var nextWeekDiffDays = now.DayOfWeek == DayOfWeek.Sunday ? 1 : 15 - (int)now.DayOfWeek;
                            return now.AddDays(nextWeekDiffDays);
                    }
                }
                return DateTime.Parse(x);
            }).FirstOrDefault();
        }

        public bool ParseBool(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);
            if (!parameters.ContainsKey(parameterName))
                return false;

            return parameters[parameterName].Where(x =>
            {
                bool outValue;
                return bool.TryParse(x, out outValue);
            }).Select(bool.Parse).FirstOrDefault();
        }

        public int ParseInteger(string searchQuery, string parameterName)
        {
            //first parse them
            TryParseValues(searchQuery);

            if (!parameters.ContainsKey(parameterName))
                return 0;

            return ParseToIntegers(searchQuery, parameterName).FirstOrDefault();
        }

        public Dictionary<string, IList<string>> ParseToDictionary(string searchQuery)
        {
            //first parse them
            TryParseValues(searchQuery);
            return parameters;
        }
    }
}