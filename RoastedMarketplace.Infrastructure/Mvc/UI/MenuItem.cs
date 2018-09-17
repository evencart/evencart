using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.Mvc.UI
{
    public class MenuItem : FoundationModel
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public string SystemName { get; set; }

        public List<MenuItem> ChildItems { get; } = new List<MenuItem>();

        public string Icon { get; set; }

        public bool OpenInNewWindow { get; set; }

        public MenuItem AddMenuItem(params MenuItem[] menuItem)
        {
            foreach (var mi in menuItem)
                ChildItems.Add(mi);
            return this;
        }
    }
}