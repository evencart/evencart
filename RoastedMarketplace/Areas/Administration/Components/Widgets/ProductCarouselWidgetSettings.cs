using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Components.Widgets
{
    public class ProductCarouselWidgetSettings : WidgetSettingsModel
    {
        public string Title { get; set; }

        public IList<int> ProductIds { get; set; }
    }
}