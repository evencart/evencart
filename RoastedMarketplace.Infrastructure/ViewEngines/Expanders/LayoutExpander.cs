using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class LayoutExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx)
        {
            var layoutMatches = regEx.Matches(readFile.Content);

            if (layoutMatches.Count == 0)
                return readFile.Content;
            if (layoutMatches.Count > 1)
                throw new Exception($"Can't use two layouts in one page");

            ExtractMatch(layoutMatches[0], out string[] straightParameters, out Dictionary<string, string> _);
            if(!straightParameters.Any())
                throw new Exception($"A layout must be specified with layout tag in view {readFile.FileName}");

            var layoutValue = straightParameters[0];
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            //read the layout now
            var layoutPath = viewAccountant.GetLayoutPath(layoutValue);
            if (layoutPath.IsNullEmptyOrWhitespace())
                throw new Exception($"Can't find layout {layoutValue} in view {readFile.FileName}");

            var layoutFile = ReadFile.From(layoutPath);
            readFile.Children.Add(layoutFile);

            //expand the layout file
            var layoutExpanded = Expand(layoutFile, regEx);
            if (layoutFile.Content == layoutExpanded)
            {
                var bodyMatcher = new Regex(@"{%\s+bodyContent\s+%}");
                //remove the layout tag
                readFile.Content = regEx.Replace(readFile.Content, "");
                //remove the body content tag
                readFile.Content = bodyMatcher.Replace(layoutExpanded, readFile.Content);
                return readFile.Content;
            }
            return string.Empty;
        }
    }
}