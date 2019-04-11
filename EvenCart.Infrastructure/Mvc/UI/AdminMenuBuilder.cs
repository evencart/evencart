using System.Collections.Generic;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Infrastructure.Mvc.UI
{
    public class AdminMenuBuilder
    {
        private readonly Dictionary<string, List<MenuItem>> _menuItems;

        private Menu _menu;
        public AdminMenuBuilder()
        {
            _menuItems = new Dictionary<string, List<MenuItem>>();
        }

        public Menu BuildMenu()
        {
            if (_menu != null)
                return _menu;
            _menu = new Menu();
            //dashboard
            var dashboardMenu = new MenuItem() {
                Text = "Dashboard"
            };

            //products
            var productsMenu = new MenuItem() {
                Text = "Products",
                Url = ApplicationEngine.RouteUrl(AdminRouteNames.ProductsList),
                SystemName = "Products"
            };

           productsMenu.AddMenuItem(new MenuItem() {
                Text = "All Products",
                Url = ApplicationEngine.RouteUrl(AdminRouteNames.ProductsList),
                SystemName = AdminRouteNames.ProductsList
            }, new MenuItem() {
                Text = "Categories",
                Url = "",
                SystemName = "admin.products.categories"
            }, new MenuItem() {
                Text = "Attributes",
                Url = "",
                SystemName = "admin.products.attributes"
            }, new MenuItem() {
                Text = "Gift Cards",
                Url = "",
                SystemName = "admin.products.giftcards"
            });

            productsMenu[0]
                .AddMenuItem(new MenuItem()
                    {
                        Text = "Basic Info",
                        Url = ApplicationEngine.RouteUrl(AdminRouteNames.GetProduct),
                        SystemName = AdminRouteNames.GetProduct
                    },
                    new MenuItem()
                    {
                        Text = "Product Attributes",
                        Url = ApplicationEngine.RouteUrl(AdminRouteNames.ProductAttributesList),
                        SystemName = AdminRouteNames.ProductAttributesList
                    },
                    new MenuItem()
                    {
                        Text = "Variants",
                        Url = "",
                        SystemName = AdminRouteNames.ProductVariantsList
                    });

            //orders
            var ordersMenu = new MenuItem() { Text = "Orders" };
            //customers
            var customersMenu = new MenuItem() { Text = "Customers" };
            //discounts
            var discounts = new MenuItem() { Text = "Discounts" };
            //plugins
            var plugins = new MenuItem() { Text = "Plugins" };
            //settings
            var settings = new MenuItem() { Text = "Settings" };

            _menu.AddMenuItem(dashboardMenu, productsMenu, ordersMenu, customersMenu, discounts, plugins, settings);

            return _menu;
        }

        public void Clear(string key)
        {
            if (_menuItems.ContainsKey(key))
                _menuItems[key].Clear();
        }

        public void Add(string key, MenuItem menuItem)
        {
           if(!_menuItems.ContainsKey(key))
                _menuItems.Add(key, new List<MenuItem>());

           _menuItems[key].Add(menuItem);
        }

        public List<MenuItem> Get(string key)
        {
            _menuItems.TryGetValue(key, out List<MenuItem> menuItems);
            return menuItems;
        }

        public static AdminMenuBuilder Instance => Singleton<AdminMenuBuilder>.Instance;
    }
}