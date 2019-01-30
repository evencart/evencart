using System.Collections.Generic;

namespace RoastedMarketplace.Infrastructure.Theming
{
    public class ThemeInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ProductBoxImageSize { get; set; }

        public string CartItemImageSize { get; set; }

        public string ProductPageImageSize { get; set; }

        public string ProductPageImageThumbnailSize { get; set; }

        public Dictionary<string, string> WidgetZones { get; set; }
    }
}