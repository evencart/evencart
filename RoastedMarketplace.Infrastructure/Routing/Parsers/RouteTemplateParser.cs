using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RoastedMarketplace.Infrastructure.Routing.Parsers
{
    public class RouteTemplateParser : IRouteTemplateParser
    {
        internal const string SeNameKey = "SeName";
        internal const string CategoryPathKey = "CategoryPath";
        internal const string IdKey = "Id";
        internal const string YearKey = "Year";
        internal const string MonthKey = "Month";
        internal const string DayKey = "Day";
        /// <summary>
        /// Stores mapping of template to corresponding regex
        /// </summary>
        private readonly Dictionary<string, string> _templateMap = new Dictionary<string, string>()
        {
            { SeNameKey, $@"(?<{SeNameKey}>([a-zA-Z0-9\-_$%\.~]+))(?:/?)" },
            { CategoryPathKey, $@"?(?<{CategoryPathKey}>[a-zA-Z0-9\-_$%\.~\/]+)*" },
            { IdKey, $@"(?<{IdKey}>[0-9]+)" },
            { YearKey, $@"(?<{YearKey}>[0-9][0-9][0-9][0-9])" },
            { MonthKey, $@"(?<{MonthKey}>([1-9]|0[1-9]|1[0-2]))" },
            { DayKey, $@"(?<{DayKey}>([1-9]|0[1-9]|[1-2][0-9]|3[0-1]))" },
        };

        private readonly Dictionary<string, Regex> _regexCache = new Dictionary<string, Regex>();

        public virtual Dictionary<string, string> ParsePathForTemplate(string path, string template)
        {
            var parsedValues = new Dictionary<string, string>();
            template = GetProcessedRouteTemplate(template);
            //perf: get and/or preserve the regex
            if (!_regexCache.TryGetValue(template, out Regex regEx))
            {
                regEx = new Regex(template);
                _regexCache.Add(template, regEx);
            }
            ParseTemplate(path, regEx, parsedValues);
            return parsedValues;
        }

        public string GetProcessedRouteTemplate(string template, WrapType wrapType = WrapType.WholeString, bool escapeBrackets = false)
        {
            foreach (var map in _templateMap)
            {
                var expectedTemplate = $"{{{map.Key}}}";
                var templatePattern = map.Value;
                if (!template.Contains(expectedTemplate))
                    continue;
                if(wrapType == WrapType.EachToken)
                    templatePattern = $"^{templatePattern}$";
                template = template.Replace(expectedTemplate, templatePattern);
            }
            if (wrapType == WrapType.WholeString)
                //append an ending to the template
                template = $"^{template}$";

            //should we escape brackets [] to [[]]
            if (escapeBrackets)
                template = template.Replace("[", "[[").Replace("]", "]]");
            return template;
        }

        private void ParseTemplate(string path, Regex regex, Dictionary<string, string> resultCollection)
        {
            var matches = regex.Matches(path);
            if (matches.Count == 0)
                return;
            foreach (Match match in matches)
            {
                if (match.Groups[SeNameKey].Captures.Any())
                {
                    AddSingleGroup(SeNameKey, match, resultCollection);
                }
                if (match.Groups[CategoryPathKey].Captures.Any())
                {
                    AddSingleGroup(CategoryPathKey, match, resultCollection);
                }
                if (match.Groups[IdKey].Captures.Any())
                {
                    AddSingleGroup(IdKey, match, resultCollection);
                }
                if (match.Groups[YearKey].Captures.Any())
                {
                    AddSingleGroup(YearKey, match, resultCollection);
                }
                if (match.Groups[MonthKey].Captures.Any())
                {
                    AddSingleGroup(MonthKey, match, resultCollection);
                }
                if (match.Groups[DayKey].Captures.Any())
                {
                    AddSingleGroup(DayKey, match, resultCollection);
                }
            }
        }

        private void AddSingleGroup(string key, Match match, Dictionary<string, string> resultCollection)
        {
            var value = match.Groups[key].Value;
            if (resultCollection.ContainsKey(key))
                resultCollection[key] = value;
            else
                resultCollection.Add(key, value);
        }
    }
}