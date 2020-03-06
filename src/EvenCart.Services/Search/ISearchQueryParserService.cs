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
using System.Collections.Generic;

namespace EvenCart.Services.Search
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