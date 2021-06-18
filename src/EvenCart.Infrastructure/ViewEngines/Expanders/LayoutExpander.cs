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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Genesis;
using Genesis.Infrastructure;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class LayoutExpander : Expander
    {
        private const string ContentWithoutLayoutKey = "ContentWithoutLayout";
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var layoutMatches = regEx.Matches(inputContent);

            if (layoutMatches.Count == 0)
            {
                if (WebHelper.IsAjaxRequest(ApplicationEngine.CurrentHttpContext.Request))
                {
                    //return content without layout in case of raw request
                    var layoutLessContent = readFile.GetMeta(nameof(LayoutExpander))
                        .FirstOrDefault(x => x.Key == ContentWithoutLayoutKey)
                        .Value?.ToString() ?? inputContent;

                    //is there any ajax layout 
                    var ajaxLayoutPath = viewAccountant.GetLayoutPath("_LayoutAjax");
                    if (!ajaxLayoutPath.IsNullEmptyOrWhiteSpace())
                    {
                        var ajaxLayout = ReadFile.From(ajaxLayoutPath);
                        return ajaxLayout.Content.Replace("{% bodyContent %}", layoutLessContent);
                    }
                    return layoutLessContent;
                }
                return inputContent;
            }
            if (layoutMatches.Count > 1)
                throw new Exception($"Can't use two layouts in one page");

            ExtractMatch(layoutMatches[0], out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
            if (!straightParameters.Any())
                throw new Exception($"A layout must be specified with layout tag in view {readFile.FileName}");
           
            if (keyValuePairs != null && keyValuePairs.Any(x => x.Key.Equals("ignoreForAjax") && x.Value == "true"))
            {
                //preserve content without layout
                readFile.AddMeta(ContentWithoutLayoutKey, regEx.Replace(readFile.Content, ""), nameof(LayoutExpander));

                if (WebHelper.IsAjaxRequest(ApplicationEngine.CurrentHttpContext.Request))
                {
                    //return content without layout in case of raw request
                    return readFile.GetMeta(nameof(LayoutExpander))
                        .FirstOrDefault(x => x.Key == ContentWithoutLayoutKey)
                        .Value.ToString();
                }
            }

            var layoutValue = straightParameters[0];
           
            //read the layout now
            var layoutPath = viewAccountant.GetLayoutPath(layoutValue);
            if (layoutPath.IsNullEmptyOrWhiteSpace())
                throw new Exception($"Can't find layout {layoutValue} in view {readFile.FileName}");

            var layoutFile = ReadFile.From(layoutPath);
            readFile.AddChild(layoutFile);

            //expand the layout file
            var layoutExpanded = Expand(layoutFile, regEx, layoutFile.Content);
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