using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class NavigationImplementation : FoundationModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Css { get; set; }

        public bool IsGroup { get; set; }

        public IList<NavigationImplementation> Children { get; set; }
    }
}