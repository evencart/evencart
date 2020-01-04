using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeInfo
    {
        public string Name { get; set; }

        public string DirectoryName { get; set; }

        public string Description { get; set; }

        public string ProductBoxImageSize { get; set; }

        public string CartItemImageSize { get; set; }

        public string ProductPageImageSize { get; set; }

        public string ProductPageImageThumbnailSize { get; set; }

        public string UserProfileImageSize { get; set; }

        public Dictionary<string, string> WidgetZones { get; set; }

        public ConcurrentDictionary<string, string> Templates { get; set; } = new ConcurrentDictionary<string, string>();

        public string ThumbnailUrl { get; set; }

        public string GetTemplatePath(string templateKey)
        {
            Templates.TryGetValue(templateKey, out var template);
            return template;
        }
    }
}