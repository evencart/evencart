using System.Collections.Generic;

namespace EvenCart.Infrastructure.Mvc.UI
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        public Menu AddMenuItem(params MenuItem[] menuItem)
        {
            foreach (var mi in menuItem)
                MenuItems.Add(mi);
            return this;
        }
    }
}