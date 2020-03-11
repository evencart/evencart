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
                {
                    if(readFile != null)
                        throw new Exception($"A route name must be specified in the view {readFile.FileName}");
                    else
                    {
                        throw new Exception($"A route name must be specified. See inner exception for content", new Exception(inputContent));
                    }
                }

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
                if (readFile != null)
                    readFile.Content = readFile.Content.Replace(match.Result("$0"), routeUrl);
                inputContent = inputContent.Replace(match.Result("$0"), routeUrl);
            }

            return inputContent;
        }
    }
}