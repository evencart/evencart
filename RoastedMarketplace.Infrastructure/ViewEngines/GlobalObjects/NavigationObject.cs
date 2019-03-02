using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Navigation;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using RoastedMarketplace.Services.Navigation;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class NavigationObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            if (generalSettings.PrimaryNavigationId <= 0)
                return null;

            var menuService = DependencyResolver.Resolve<IMenuService>();
            var menu = menuService.Get(generalSettings.PrimaryNavigationId);
            if (menu == null)
                return null;
            return GetNavigationImpl(menu.MenuItems, 0);
        }

        ///Making this method static so we can use the same method in MenuWidget.
        /// todo: is there a better way of doing this? may be move this to a helper function
        public static IList<NavigationImplementation> GetNavigationImpl(IList<MenuItem> menuItems, int parentMenuItemId)
        {
            if (menuItems == null)
                return null;
            var navigation = new List<NavigationImplementation>();
            foreach (var menuItem in menuItems.Where(x => x.ParentMenuItemId == parentMenuItemId))
            {
                if (parentMenuItemId == 0 && menuItem.IsGroup)
                    continue;
                var url = menuItem.SeoMeta == null ? menuItem.Url : SeoMetaHelper.GetUrl(menuItem.SeoMeta);
                var title = menuItem.Name;
                var navigationItem = new NavigationImplementation
                {
                    Url = url,
                    Title = title,
                    IsGroup = menuItem.IsGroup,
                    Css = menuItem.CssClass,
                    Children = GetNavigationImpl(menuItems, menuItem.Id) ??
                               new List<NavigationImplementation>()
                };
                navigation.Add(navigationItem);
            }
            return navigation;
        }
    }
}