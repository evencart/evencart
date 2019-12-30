using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Navigation;
using EvenCart.Services.Products;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using EvenCart.Services.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
{
    public class NavigationObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            if (generalSettings.PrimaryNavigationId <= 0)
                return null;

            var menuService = DependencyResolver.Resolve<IMenuService>();
            var categoryService = DependencyResolver.Resolve<ICategoryService>();
            var menu = menuService.Get(generalSettings.PrimaryNavigationId);
            if (menu == null)
                return null;
            var allCategories = categoryService.GetFullCategoryTree();
            return GetNavigationImpl(menu.MenuItems, 0, allCategories);
        }

        ///Making this method static so we can use the same method in MenuWidget.
        /// todo: is there a better way of doing this? may be move this to a helper function
        public static IList<NavigationImplementation> GetNavigationImpl(IList<MenuItem> menuItems, int parentMenuItemId, IList<Category> categories)
        {
            if (menuItems == null)
                return null;
            var navigation = new List<NavigationImplementation>();
            foreach (var menuItem in menuItems.Where(x => x.ParentMenuItemId == parentMenuItemId))
            {
                if (parentMenuItemId == 0 && menuItem.IsGroup)
                    continue;
                string categoryPath = null;

                var url = menuItem.SeoMeta == null ? menuItem.Url : null;
                if (url == null && menuItem.SeoMeta != null)
                {
                    if (menuItem.SeoMeta.EntityName == nameof(Category))
                    {
                        var category = categories.First(x => x.Id == menuItem.SeoMeta.EntityId);
                        categoryPath = category.GetCategoryPath(categories);
                    }
                    url = SeoMetaHelper.GetUrl(menuItem.SeoMeta, categoryPath);
                }
               
                var title = menuItem.Name;
                var navigationItem = new NavigationImplementation
                {
                    Url = url,
                    Title = title,
                    IsGroup = menuItem.IsGroup,
                    Css = menuItem.CssClass,
                    Children = GetNavigationImpl(menuItems, menuItem.Id, categories) ??
                               new List<NavigationImplementation>(),
                    Id = menuItem.Id
                };
                navigation.Add(navigationItem);
            }
            return navigation;
        }

        public override bool RenderInAdmin => false;

        public override bool RenderInPublic => true;
    }
}