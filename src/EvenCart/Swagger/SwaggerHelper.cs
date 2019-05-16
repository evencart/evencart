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