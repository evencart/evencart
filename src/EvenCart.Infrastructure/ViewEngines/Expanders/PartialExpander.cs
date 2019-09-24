using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
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
                var includeAll = false;
                if (keyValuePairs != null)
                {
                    keyValuePairs.TryGetValue("includeAll", out var iaStr);
                    includeAll = iaStr == "true";
                }
                var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();

                Func<string, string> partialReplaceAction = s =>
                {
                    var viewFile = ReadFile.From(s);
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

                        return assignString + partialViewExpanded + resetAssignString;
                    }

                    return "";
                };

                var partialExpanded = "";
                if (includeAll)
                {
                    var allFiles = viewAccountant.GetAllMatchingViewPaths(partialFile);
                    foreach (var file in allFiles)
                        partialExpanded = partialExpanded + partialReplaceAction(file);
                }
                else
                {
                    //read the layout now
                    var viewPath = viewAccountant.GetThemeViewPath(partialFile);
                    if (viewPath.IsNullEmptyOrWhiteSpace())
                        throw new Exception($"Can't find partial view {partialFile} in view {readFile.FileName}");
                    partialExpanded = partialReplaceAction(viewPath);
                }
                inputContent = inputContent.Replace(partialMatch.Result("$0"),
                    partialExpanded);
                readFile.Content = readFile.Content.Replace(partialMatch.Result("$0"),
                    partialExpanded);
            }
            return inputContent;
        }
    }
}