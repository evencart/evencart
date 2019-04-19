using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.UI;

namespace EvenCart.Infrastructure.ViewEngines.Expanders
{
    public class NavigationExpander : Expander
    {
        public string NavigationType { get; set; }
        public override string Expand(ReadFile readFile, Regex regEx, string inputContent, object parameters = null)
        {
            if (!ApplicationEngine.IsAdmin())
                return inputContent;
            var matches = regEx.Matches(inputContent);
            var paramsAsDict = (IDictionary<string, object>)parameters;
            if (paramsAsDict == null)
                return inputContent;
            if (matches.Count == 0)
            {
                var navMeta = readFile.GetMeta(nameof(NavigationExpander)).FirstOrDefault(x => x.Key == "navigation_" + NavigationType);
                if (navMeta.Key != null)
                    paramsAsDict?.Add(NavigationType, navMeta.Value);
                var groupMeta = readFile.GetMeta(nameof(NavigationExpander)).FirstOrDefault(x => x.Key == "group_" + NavigationType);
                if (navMeta.Key != null)
                    paramsAsDict?.Add(NavigationType + "_groups", groupMeta.Value);
                return inputContent;
            }

            List<Navigation> menuList = null;
            if (!paramsAsDict.ContainsKey(NavigationType))
            {
                menuList = new List<Navigation>();
                paramsAsDict.Add(NavigationType, menuList);
            }

            List<NavigationGroup> groupList = null;
            if (!paramsAsDict.ContainsKey("group_" + NavigationType))
            {
                groupList = new List<NavigationGroup>()
                {
                    new NavigationGroup() { Name = "", Id = null , DisplayOrder = 0}
                };
                paramsAsDict.Add("group_" + NavigationType, groupList);
            }

            menuList = (List<Navigation>)paramsAsDict[NavigationType];


            foreach (Match match in matches)
            {
                ExtractMatch(match, out var _, out var keyValuePairs);

                //first for groups
                keyValuePairs.TryGetValue("group", out var group);
                keyValuePairs.TryGetValue("order", out var displayOrderValue);
                keyValuePairs.TryGetValue("id", out var id);
                int.TryParse(displayOrderValue, out var displayOrder);

                if (group != null)
                {
                    groupList.Add(new NavigationGroup()
                    {
                        Name = group,
                        DisplayOrder = displayOrder,
                        Id = id
                    });

                    continue;
                }
                //do we have route parameter?
                keyValuePairs.TryGetValue("url", out var url);
                keyValuePairs.TryGetValue("title", out var title);
                keyValuePairs.TryGetValue("systemName", out var systemName);
                keyValuePairs.TryGetValue("groupId", out var groupId);
                keyValuePairs.TryGetValue("capability", out var capability);
                keyValuePairs.TryGetValue("iconClass", out var iconClass);
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
                   DisplayOrder = displayOrder,
                   GroupId = groupId,
                   Capabilities = capability?.Split(" or ", StringSplitOptions.RemoveEmptyEntries),
                   IconClass = iconClass
                });
            }

            menuList = menuList.OrderBy(x => x.DisplayOrder).ToList();
            paramsAsDict[NavigationType] = menuList;
            readFile.AddMeta($"navigation_" + NavigationType, menuList, $"{nameof(NavigationExpander)}");

            groupList = groupList.OrderBy(x => x.DisplayOrder).ToList();
            paramsAsDict[NavigationType + "_groups"] = groupList;
            readFile.AddMeta($"group_" + NavigationType, groupList, $"{nameof(NavigationExpander)}");
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

            public string GroupId { get; set; }

            public string[] Capabilities { get; set; }

            public string IconClass { get; set; }
        }

        internal class NavigationGroup : FoundationModel
        {
            public string Name { get; set; }

            public int DisplayOrder { get; set; }

            public string Id { get; set; }
        }
    }
}