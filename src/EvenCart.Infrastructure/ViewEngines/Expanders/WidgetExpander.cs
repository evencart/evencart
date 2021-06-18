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
using System.Text;
using System.Text.RegularExpressions;
using Genesis.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class WidgetExpander : Expander
    {
        private const string ComponentFormat = @"component ""{0}"" {1}";
        
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var widgetMatches = regEx.Matches(inputContent);
            if (widgetMatches.Count == 0)
            {
                return inputContent;
            }
            var pluginSettings = DependencyResolver.Resolve<PluginSettings>();
            var widgets = pluginSettings.GetSiteWidgets(true);
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            
            var widgetFormat = "{0}";
            //do we have a wrapper?
            var widgetPath = viewAccountant.GetThemeViewPath("Widgets/Index");
            if (!widgetPath.IsNullEmptyOrWhiteSpace())
            {
                var widgetFile = ReadFile.From(widgetPath);
                widgetFormat = widgetFile.Content;
            }
            foreach (Match widgetMatch in widgetMatches)
            {
                ExtractMatch(widgetMatch, out string[] straightParameters, out Dictionary<string, string> keyValuePairs);
                if (!straightParameters.Any())
                    throw new Exception($"A widget must be specified with zone name in view {readFile.FileName}");

                var zoneName = straightParameters[0];
                var widgetBuilder = new StringBuilder();
                var paramBuilder = new StringBuilder();
                keyValuePairs = keyValuePairs ?? new Dictionary<string, string>();
                foreach (var kp in keyValuePairs)
                    paramBuilder.Append($"{kp.Key}=\"{kp.Value}\" ");
                foreach (var widget in widgets.Where(x => x.ZoneName == zoneName))
                {
                    var idStr = $"id=\"{widget.Id}\"";
                    var componentStr =
                        $"{{% {string.Format(ComponentFormat, widget.WidgetSystemName, paramBuilder)} {idStr} %}}";
                    var widgetStr = string.Format(widgetFormat, componentStr);
                    widgetBuilder.Append(widgetStr);
                    widgetBuilder.AppendLine();
                }
                inputContent = inputContent.Replace(widgetMatch.Result("$0"), widgetBuilder.ToString());
                readFile.Content = readFile.Content.Replace(widgetMatch.Result("$0"), widgetBuilder.ToString());
            }
            return inputContent;
        }
    }
}