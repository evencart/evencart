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
        public override string Expand(ReadFile readFile, Regex regEx)
        {
            var partialMatches = regEx.Matches(readFile.Content);

            if (partialMatches.Count == 0)
                return readFile.Content;

            foreach (Match partialMatch in partialMatches)
            {
                ExtractMatch(partialMatch, out string[] straightParameters, out Dictionary<string, string> _);
                if(!straightParameters.Any())
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
                var partialViewExpanded = Expand(viewFile, regEx);
                if (viewFile.Content == partialViewExpanded)
                {
                    readFile.Content = readFile.Content.Replace(partialMatch.Result("$0"), partialViewExpanded);
                }
            }
            return readFile.Content;
        }
    }
}