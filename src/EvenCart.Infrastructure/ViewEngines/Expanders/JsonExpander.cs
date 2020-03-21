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

using System.Collections.Generic;
using System.Text.RegularExpressions;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;

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