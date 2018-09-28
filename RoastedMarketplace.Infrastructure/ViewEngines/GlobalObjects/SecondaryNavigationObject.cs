using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Infrastructure.Mvc.UI;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class SecondaryNavigationObject : GlobalObject
    {
        private Menu _menu = null;
        public override object GetObject()
        {
            return AdminMenuBuilder.Instance.Get(ApplicationConfig.SecondaryNavigationName);
        }

        private List<MenuItem> GetMenuItems()
        {
            //we only return menu items which are specific to current route
            var currentRouteName = ApplicationEngine.GetActiveRouteName();
            MenuItem activeMenuItem = null;
            foreach (var menuItem in _menu.MenuItems)
            {
                if (activeMenuItem != null)
                    break;
                activeMenuItem = FindMenuItem(menuItem, currentRouteName);
            }
            return activeMenuItem?.GetParent().ChildItems;

        }

        private MenuItem FindMenuItem(MenuItem menuItem, string routeName)
        {
            if (menuItem.SystemName == routeName)
                return menuItem;
            foreach (var menuItemChildItem in menuItem.ChildItems)
            {
                return FindMenuItem(menuItemChildItem, routeName);
            }
            return null;
        }
    }
}