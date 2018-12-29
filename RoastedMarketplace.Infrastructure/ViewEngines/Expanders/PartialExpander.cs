using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class PartialExpander : Expander
    {
        private const string AssignFormat = "{{%- capture {0} -%}}{1}{{%- endcapture -%}}";

        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var partialMatches = regEx.Matches(inputContent);

            if (partialMatches.Count == 0)
                return inputContent;

            foreach (Match partialMatch in partialMatches)
            {
                ExtractMatch(partialMatch, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if (!straightParameters.Any())
                    throw new Exception($"A partial view must be specified with partial tag in view {readFile.FileName}");

                var partialFile = straightParameters[0];

                var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
                //read the layout now
                var viewPath = viewAccountant.GetThemeViewPath(partialFile);
                if (viewPath.IsNullEmptyOrWhitespace())
                    throw new Exception($"Can't find partial view {partialFile} in view {readFile.FileName}");

                var viewFile = ReadFile.From(viewPath);
                readFile.AddChild(viewFile);

                //expand the view file
                var partialViewExpanded = Expand(viewFile, regEx, viewFile.Content);
                if (viewFile.Content == partialViewExpanded)
                {
                    var assignString = "";
                    var resetAssignString = "";
                    if (keyValuePairs != null)
                    {
                        //create assigns
                        assignString = string.Join("",
                            keyValuePairs.Select(x => string.Format(AssignFormat, x.Key, x.Value)));
                        resetAssignString = string.Join("",
                            keyValuePairs.Select(x => string.Format(AssignFormat, x.Key, "")));
                    }
                    inputContent = inputContent.Replace(partialMatch.Result("$0"), assignString + partialViewExpanded + resetAssignString);
                    readFile.Content = readFile.Content.Replace(partialMatch.Result("$0"), assignString + partialViewExpanded + resetAssignString);
                }
               

            }
            return inputContent;
        }
    }
}