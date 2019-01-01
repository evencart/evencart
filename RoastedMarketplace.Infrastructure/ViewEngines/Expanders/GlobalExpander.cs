using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class GlobalExpander : Expander
    {
        private const string AssignFormat = "{{%- capture {0} -%}}{1}{{%- endcapture -%}}";
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            if (matches.Count == 0)
                return inputContent;
            var globalBuilder = new StringBuilder();
            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] _, out Dictionary<string, string> keyValuePairs);
                var ifStr = "";
                var endifStr = "";
                if (keyValuePairs.ContainsKey("if"))
                {
                    ifStr = $"{{% if {keyValuePairs["if"]} %}}";
                    endifStr = "{% endif %}";
                }
                keyValuePairs.Remove("if");
                foreach (var parameter in keyValuePairs)
                {
                    var variableName = parameter.Key;
                    var variableValue = parameter.Value;
                    globalBuilder.Append(ifStr);
                    globalBuilder.Append(string.Format(AssignFormat, variableName, variableValue));
                    globalBuilder.Append(endifStr);
                    globalBuilder.Append(Environment.NewLine);
                }
            }
            //remove the global matches
            readFile.Content = regEx.Replace(readFile.Content, "");
            inputContent = regEx.Replace(inputContent, "");
            //and prepend the assignments
            readFile.Content = globalBuilder + readFile.Content;
            inputContent = globalBuilder + inputContent;
            return inputContent;
        }
    }
}