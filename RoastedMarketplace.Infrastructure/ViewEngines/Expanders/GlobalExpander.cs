using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class GlobalExpander : Expander
    {
        private const string AssignFormat = "{{%- assign {0} = \"{1}\" -%}}";
        public override string Expand(ReadFile readFile, Regex regEx)
        {
            var matches = regEx.Matches(readFile.Content);
            if (matches.Count == 0)
                return readFile.Content;
            var globalBuilder = new StringBuilder();
            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] _, out Dictionary<string, string> keyValuePairs);
                foreach (var parameter in keyValuePairs)
                {
                    var variableName = parameter.Key;
                    var variableValue = parameter.Value;
                    globalBuilder.Append(string.Format(AssignFormat, variableName, variableValue));
                    globalBuilder.Append(Environment.NewLine);
                }
            }
            //remove the global matches
            readFile.Content = regEx.Replace(readFile.Content, "");
            //and prepend the assignments
            readFile.Content = globalBuilder + readFile.Content;
            return readFile.Content;
        }
    }
}