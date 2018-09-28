using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Infrastructure.Mvc.UI;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class PrimaryNavigationObject : GlobalObject
    {
        private Menu _menu = null;
        public override object GetObject()
        {
            return AdminMenuBuilder.Instance.Get(ApplicationConfig.PrimaryNavigationName);
        }

        private List<MenuItem> GetMenuItems()
        {
            //we only return menu items which are specific to current route
            var currentRouteName = ApplicationEngine.GetActiveRouteName();
            var activeMenuItem = _menu.MenuItems.SelectMany(x => x.ChildItems)
                .FirstOrDefault(x => x.SystemName == currentRouteName);
            if (activeMenuItem == null)
                //return everything
                return _menu.MenuItems;

            var resultList = new List<MenuItem>();
            var parentMenuItem = activeMenuItem.GetParent();
            foreach (var menuItem in _menu.MenuItems)
            {
                if (menuItem.SystemName == parentMenuItem.SystemName)
                {
                    resultList.Add(new MenuItem()
                    {
                        
                    });
                }
            }
            return _menu.MenuItems;

        }
    }
}