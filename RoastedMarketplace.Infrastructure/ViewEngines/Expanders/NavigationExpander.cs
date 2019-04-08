using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.UI;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Expanders
{
    public class NavigationExpander : Expander
    {
        public string NavigationType { get; set; }
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            var matches = regEx.Matches(inputContent);
            var paramsAsDict = (IDictionary<string, object>)parameters;
            if (matches.Count == 0)
            {
                var navMeta = readFile.GetMeta(nameof(NavigationExpander)).FirstOrDefault(x => x.Key == "navigation_" + NavigationType);
                if (navMeta.Key != null)
                    paramsAsDict?.Add(NavigationType, navMeta.Value);
                return inputContent;
            }
        
            if (paramsAsDict == null)
                return inputContent;

            List<Navigation> menuList = null;
            if (!paramsAsDict.ContainsKey(NavigationType))
            {
                menuList = new List<Navigation>();
                paramsAsDict.Add(NavigationType, menuList);
            }

            menuList = (List<Navigation>)paramsAsDict[NavigationType];
            //it's not possible to preserve and serve different navigation because of cached views and clearing
            //of navigation on each request. 
            //as a workaround, we create a string pattern and spit it no the liquid page
            //then liquid uses that variable to render the menus

            foreach (Match match in matches)
            {
                ExtractMatch(match, out string[] _, out Dictionary<string, string> keyValuePairs);

                //do we have route parameter?
                keyValuePairs.TryGetValue("url", out string url);
                keyValuePairs.TryGetValue("title", out string title);
                keyValuePairs.TryGetValue("systemName", out string systemName);
                keyValuePairs.TryGetValue("order", out string displayOrderValue);
                int.TryParse(displayOrderValue, out int displayOrder);
                if (url.IsNullEmptyOrWhiteSpace())
                {
                    //use the current url if it's empty url
                    url = ApplicationEngine.CurrentHttpContext.Request.Path + ApplicationEngine.CurrentHttpContext.Request.QueryString;
                }

                title = title ?? "";
                systemName = systemName ?? "";
                menuList.Add(new Navigation()
                {
                   Title = title,
                   Url = url,
                   SystemName = systemName,
                   DisplayOrder = displayOrder
                });
            }

            var orderedMenuList = menuList.OrderBy(x => x.DisplayOrder).ToList();
            paramsAsDict[NavigationType] = orderedMenuList;
            readFile.AddMeta($"navigation_" + NavigationType, orderedMenuList, $"{nameof(NavigationExpander)}");
            //remove the tags
            readFile.Content = regEx.Replace(readFile.Content, "");
            inputContent = regEx.Replace(inputContent, "");
            return inputContent;
        }

        public override void PreRun(ReadFile readFile)
        {
            //clear the menu
            AdminMenuBuilder.Instance.Clear(NavigationType);
        }

        internal class Navigation : FoundationModel
        {
            public string Title { get; set; }

            public string Url { get; set; }

            public int DisplayOrder { get; set; }

            public string SystemName { get; set; }
        }
    }
}