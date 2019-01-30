using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Mvc.UI;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class NavigationExpander : Expander
    {
        public string NavigationType { get; set; }
        private const string AssignFormat = "{{%- assign {0} = {1} | split : \"" + ItemSeparator + "\" -%}}";
        private const string CaptureFormat = "{{%- capture {0} -%}}{1}{{%- endcapture -%}}";

        private const string ColumnSeparator = "::::";
        private const string ItemSeparator = "=%=";
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            if (matches.Count == 0)
                return inputContent;

            //it's not possible to preserve and serve different navigation because of cached views and clearing
            //of navigation on each request. 
            //as a workaround, we create a string pattern and spit it no the liquid page
            //then liquid uses that variable to render the menus
            var captureBuilder = new StringBuilder();

            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] _, out Dictionary<string, string> keyValuePairs);

                //do we have route parameter?
                keyValuePairs.TryGetValue("url", out string url);
                keyValuePairs.TryGetValue("title", out string title);
                keyValuePairs.TryGetValue("systemName", out string systemName);

                if (url.IsNullEmptyOrWhiteSpace())
                {
                    //use the current url if it's empty url
                    url = ApplicationEngine.CurrentHttpContext.Request.Path + ApplicationEngine.CurrentHttpContext.Request.QueryString;
                }
                
                title = title ?? "";
                systemName = systemName ?? "";
                //single navigation link
                captureBuilder.Append($"{title}{ColumnSeparator}{url}{ColumnSeparator}{systemName}");
                //single item completion
                captureBuilder.Append(ItemSeparator);
            }
            //remove the tags
            readFile.Content = regEx.Replace(readFile.Content, "");
            inputContent = regEx.Replace(inputContent, "");
            //prepend our variable
            var captureStatement = string.Format(CaptureFormat, $"{NavigationType}_temporary", captureBuilder.ToString());
            var assignStatement = string.Format(AssignFormat, NavigationType, $"{NavigationType}_temporary");
            readFile.Content = captureStatement + assignStatement + readFile.Content;
            inputContent = captureStatement + assignStatement + inputContent;
            return inputContent;
        }

        public override void PreRun(ReadFile readFile)
        {
            //clear the menu
            AdminMenuBuilder.Instance.Clear(NavigationType);
        }
    }
}