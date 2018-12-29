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
        private static readonly string[] NonAttributeNames = {"items", "value", "text", "for"};
        private const string AssignFormat = "{{%- assign {0} -%}}";
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            if (!matches.Any())
                return inputContent;
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
                readFile.AddChild(controlFile);
                var keyValuePairsString = keyValuePairs.Where(x => !NonAttributeNames.Contains(x.Key))
                    .Select(x => $"{x.Key}=\"{x.Value}\"").ToList();
                var attributeString = string.Join(" ", keyValuePairsString);
                var assigns = string.Join("", keyValuePairsString.Select(x => string.Format(AssignFormat, x)));
                //replace attribute string
                var controlText = controlFile.Content.Replace("%attributes%", attributeString);
                foreach(var kp in keyValuePairs.Where(x => NonAttributeNames.Contains(x.Key)))
                {
                    controlText = controlText.Replace($"%{kp.Key}%", kp.Value);
                }

                //replace the non attributes which were not passed
                foreach(var nan in NonAttributeNames)
                    controlText = controlText.Replace($"%{nan}%", "");
                var resetAssigns = string.Join("",
                    keyValuePairs.Select(x => string.Format(AssignFormat, $"{x.Key}=\"\"")).ToList());
                
                readFile.Content = readFile.Content.Replace(match.Result("$0"), assigns + controlText + resetAssigns);
                inputContent = inputContent.Replace(match.Result("$0"), assigns + controlText + resetAssigns);
            }

            return inputContent;
        }
    }
}