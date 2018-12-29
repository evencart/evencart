using System;
using System.Collections.Generic;

namespace RoastedMarketplace.Services.Search
{
    /// <summary>
    /// Parses a search query string and extracts values for parameters e.g. status:active,passive+agents:1,2
    /// </summary>
    public interface ISearchQueryParserService
    {

        int[] ParseToIntegers(string searchQuery, string parameterName);

        string[] ParseToStrings(string searchQuery, string parameterName);

        string ParseSearchText(string searchQuery);

        string ParseString(string searchQuery, string parameterName);

        decimal? ParseDecimal(string searchQuery, string parameterName);

        DateTime? ParseDateTime(string searchQuery, string parameterName);

        bool ParseBool(string searchQuery, string parameterName);

        int ParseInteger(string searchQuery, string parameterName);

        Dictionary<string, IList<string>> ParseToDictionary(string searchQuery);
    }
}