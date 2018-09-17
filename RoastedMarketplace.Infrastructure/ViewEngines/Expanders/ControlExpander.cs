using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class ControlExpander : Expander
    {
        private static string[] NonAttributeNames = {"items", "value", "text"};
        public override string Expand(ReadFile readFile, Regex regEx)
        {
            var matches = regEx.Matches(readFile.Content);
            if (!matches.Any())
                return readFile.Content;
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            
            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if (!straightParameters.Any())
                    throw new Exception($"No control type specified in the tag in view file {readFile.FileName}");

                var controlType = straightParameters[0];
                var viewName = viewAccountant.GetThemeViewPath($"Controls/{controlType}");
                if (viewName.IsNullEmptyOrWhitespace())
                    throw new Exception($"Can't find the view {viewName} in view file {readFile.FileName}");
                var controlFile = ReadFile.From(viewName);
                readFile.Children.Add(controlFile);
                var attributeString = string.Join(" ", keyValuePairs.Where(x => !NonAttributeNames.Contains(x.Key))
                    .Select(x => $"{x.Key}=\"{x.Value}\""));
                //replace attribute string
                var controlText = controlFile.Content.Replace("{% attributes %}", attributeString);
                foreach(var kp in keyValuePairs.Where(x => NonAttributeNames.Contains(x.Key)))
                {
                    controlText = controlText.Replace($"{{% {kp.Key} %}}", kp.Value);
                }

                //replace the non attributes which were not passed
                foreach(var nan in NonAttributeNames)
                    controlText = controlText.Replace($"{{% {nan} %}}", "");

                readFile.Content = readFile.Content.Replace(match.Result("$0"), controlText);
            }

            return readFile.Content;
        }
    }
}