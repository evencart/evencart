using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class StoreImplementation : FoundationModel
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public ThemeImplementation Theme { get; set; }

        public string LogoUrl { get; set; }

        public string CurrentPage { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public bool WishlistEnabled { get; set; }

        public bool RepeatOrdersEnabled { get; set; }

        public bool ReviewsEnabled { get; set; }

        public bool RelatedProductsEnabled { get; set; }

        public bool ChangeCurrencyEnabled { get; set; }
    }
}