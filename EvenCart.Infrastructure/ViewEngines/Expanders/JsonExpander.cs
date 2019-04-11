using System.Collections.Generic;
using System.Text.RegularExpressions;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class JsonExpander : Expander
    {
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            if (matches.Count == 0)
                return inputContent;
            var paramsAsDict = (IDictionary<string, object>)parameters;
            if (paramsAsDict == null)
                return inputContent;
            var serializer = DependencyResolver.Resolve<IDataSerializer>();
            foreach (Match jsonMatch in matches)
            {
                ExtractMatch(jsonMatch, out var _, out var keyValuePairs);
                keyValuePairs.TryGetValue("source", out var source);
                keyValuePairs.TryGetValue("var", out var targetVar);
                paramsAsDict.TryGetValue(source, out var sourceObj);
               
                var match = jsonMatch.Result("$0");
                var json = serializer.Serialize(sourceObj);
                var scriptedJson = $"<script type='text/javascript'>var {targetVar}={json};</script>";
                readFile.Content = readFile.Content.ReplaceFirstOccurance(match, scriptedJson);
                inputContent = inputContent.ReplaceFirstOccurance(match, scriptedJson);
            }
            //remove the global matches
            readFile.Content = regEx.Replace(readFile.Content, "");
            inputContent = regEx.Replace(inputContent, "");
            return inputContent;
        }
    }
}