using System;
using System.Drawing;
using System.Linq;

namespace RoastedMarketplace.Infrastructure.Theming
{
    public class ThemeInfo
    {
        public string ThemeName { get; set; }

        public string ThumbnailSize { get; set; }

        public string GetThemePath()
        {
            return ApplicationEngine.MapPath($"~/Content/Themes/{ThemeName}");
        }

        public Size GetThumbnailSize()
        {
            return GetSize(ThumbnailSize);
        }

        private Size GetSize(string sizeString)
        {
            var sizeParts = sizeString.Split('x', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x)).ToArray();
            if (sizeParts.Length != 2)
                throw new Exception($"Invalid size value {sizeString} in theme configuration");

            return new Size(sizeParts[0], sizeParts[1]);
        }
    }
}