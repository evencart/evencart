using RoastedMarketplace.Infrastructure.Mvc.UI;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class NavigationObject : GlobalObject
    {
        private Menu _menu = null;
        public override object GetObject()
        {
            if (_menu != null)
                return _menu.MenuItems;
            _menu = new Menu();
            (new AdminMenuBuilder()).BuildMenu(_menu);
            return _menu.MenuItems;
        }
    }
}