using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class UrlRouteExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            if (matches.Count == 0)
                return inputContent;

            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if (!straightParameters.Any())
                    throw new Exception($"A route name must be specified in the view {readFile.FileName}");

                var routeName = straightParameters[0];
                var absoluteValue = "false";
                if (keyValuePairs != null)
                {
                    keyValuePairs.TryGetValue("absolute", out absoluteValue);
                    if (!absoluteValue.IsNullEmptyOrWhiteSpace())
                        keyValuePairs.Remove("absolute");
                }
                var routeUrl = ApplicationEngine.RouteUrl(routeName, keyValuePairs, absoluteValue == "true");
                routeUrl = HttpUtility.UrlDecode(routeUrl);
                //we are using lower case urls and this causes the liquid parameters to convert to lower case like {{userId}} converts to {{userid}}
                //we need to fix those otherwise the liquid urls break.
                if (keyValuePairs != null && !routeUrl.IsNullEmptyOrWhiteSpace())
                    foreach (var kp in keyValuePairs)
                        routeUrl = routeUrl.Replace(kp.Value, kp.Value, StringComparison.InvariantCultureIgnoreCase);
                readFile.Content = readFile.Content.Replace(match.Result("$0"), routeUrl);
                inputContent = inputContent.Replace(match.Result("$0"), routeUrl);
            }

            return inputContent;
        }

    }
}