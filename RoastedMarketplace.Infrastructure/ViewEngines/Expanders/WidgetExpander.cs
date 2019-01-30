using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Extensions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
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
            var widgets = pluginSettings.GetSiteWidgets();
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
                    widgetBuilder.Append($"{{% { string.Format(ComponentFormat, widget.WidgetSystemName, paramBuilder)} {idStr} %}}");
                    widgetBuilder.AppendLine();
                }
                inputContent = inputContent.Replace(widgetMatch.Result("$0"), widgetBuilder.ToString());
                readFile.Content = readFile.Content.Replace(widgetMatch.Result("$0"), widgetBuilder.ToString());
            }
            return inputContent;
        }
    }
}