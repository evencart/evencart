using RoastedMarketplace.Infrastructure.Routing;

namespace RoastedMarketplace.Infrastructure.Mvc.UI
{
    public class AdminMenuBuilder : IMenuBuilder
    {
        public void BuildMenu(Menu menu)
        {
            //dashboard
            var dashboardMenu = new MenuItem() {
                Text = "Dashboard"
            };

            //products
            var productsMenu = new MenuItem()
            {
                Text = "Products",
                Url = "",
                SystemName = "admin.products"
            };
            productsMenu.AddMenuItem(new MenuItem()
            {
                Text = "All Products",
                Url = ApplicationEngine.RouteUrl(AdminRouteNames.ProductsList),
                SystemName = "admin.catalog.products"
            }, new MenuItem()
            {
                Text = "Categories",
                Url = "",
                SystemName = "admin.catalog.categories"
            }, new MenuItem() {
                Text = "Attributes",
                Url = "",
                SystemName = "admin.catalog.attributes"
            }, new MenuItem() {
                Text = "Gift Cards",
                Url = "",
                SystemName = "admin.catalog.giftcards"
            });

            //orders
            var ordersMenu = new MenuItem() { Text = "Orders"};
            //customers
            var customersMenu = new MenuItem() {Text = "Customers"};
            //discounts
            var discounts = new MenuItem() {Text = "Discounts"};
            //plugins
            var plugins = new MenuItem() { Text = "Plugins" };
            //settings
            var settings = new MenuItem() { Text = "Settings" };

            menu.AddMenuItem(dashboardMenu, productsMenu, ordersMenu, customersMenu, discounts, plugins, settings);
            
        }
    }
}