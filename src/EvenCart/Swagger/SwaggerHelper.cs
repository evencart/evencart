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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EvenCart.Swagger
{
    public class SwaggerHelper
    {
        private static Regex CrefMatch;
        public static IList<string> GetCrefTypes(string description)
        {
            CrefMatch = CrefMatch ?? new Regex("<see\\s+cref=\"T:([^\"]+)\">(\\w+)</see>");
            var typeNames = new List<string>();
            if (description == null)
                return typeNames;
            var matches = CrefMatch.Matches(description);
            if (matches.Count == 0)
                return typeNames;
            foreach (Match match in matches)
            {
                var typeName = match.Groups[1].Value;
                typeNames.Add(typeName);
            }
            return typeNames;
        }
    }
}