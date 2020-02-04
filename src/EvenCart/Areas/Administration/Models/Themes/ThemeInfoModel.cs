using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Themes
{
    public class ThemeInfoModel : FoundationModel
    {
        public string Name { get; set; }

        public string DirectoryName { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool PendingRestart { get; set; }
    }
}