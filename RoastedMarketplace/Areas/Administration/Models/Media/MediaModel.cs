using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Media
{
    public class MediaModel : FoundationEntityModel
    {
        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string ThumbnailUrl { get; set; }

        public string MimeType { get; set; }

        public int DisplayOrder { get; set; }
    }
}