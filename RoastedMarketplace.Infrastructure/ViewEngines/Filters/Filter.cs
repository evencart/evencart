using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
namespace RoastedMarketplace.Infrastructure.ViewEngines.Filters
{
    public abstract class Filter
    {
        public string FilterName { get; set; }

        private const string FilterMatcherPattern = @"{{\s*""([^""]*)""\s*(?:\|\s*([\w]+)\s*)+}}";
        private static readonly Regex FilterMatcher = new Regex(FilterMatcherPattern);

        private static readonly List<Filter> RegisteredFiltered = new List<Filter>()
        {
            new TranslateFilter() { FilterName = "t"}
        };

        public static string RunAll(string input)
        {
            var matches = FilterMatcher.Matches(input);
            if (!matches.Any())
                return input;
            foreach (Match match in matches)
            {
                var content = match.Groups[1].Value;
                var filters = match.Groups[2].Captures.Select(x => x.Value);
                List<string> untouchedFilters = null;
                foreach (var filterName in filters)
                {
                    var filter = RegisteredFiltered.FirstOrDefault(x => x.FilterName == filterName);
                    if (filter == null)
                    {
                        untouchedFilters = untouchedFilters ?? new List<string>();
                        untouchedFilters.Add(filterName);
                        continue;
                    }
                    content = filter.Convert(content);
                }

                if (untouchedFilters != null && untouchedFilters.Any())
                {
                    //recreate filters so as to be resolved by liquid itself
                    content = $"{{{{\"{content}\" | {string.Join('|', untouchedFilters)} }}}}";
                }
                input = input.Replace(match.Result("$0"), content);
            }
            return input;
        }

        public abstract string Convert(string input);
    }
}