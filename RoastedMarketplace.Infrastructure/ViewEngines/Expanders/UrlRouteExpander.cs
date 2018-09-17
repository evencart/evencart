using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class UrlRouteExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx)
        {
            var matches = regEx.Matches(readFile.Content);
            if (matches.Count == 0)
                return readFile.Content;

            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if(!straightParameters.Any())
                    throw new Exception($"A route name must be specified in the view {readFile.FileName}");

                var routeName = straightParameters[0];
                var routeUrl = ApplicationEngine.RouteUrl(routeName, keyValuePairs);
                routeUrl = HttpUtility.UrlDecode(routeUrl);
                readFile.Content = readFile.Content.Replace(match.Result("$0"), routeUrl);
            }

            return readFile.Content;
        }

    }
}