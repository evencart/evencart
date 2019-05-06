using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Media
{
    public class MediaModel : FoundationEntityModel
    {
        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string ThumbnailUrl { get; set; }

        public string MimeType { get; set; }

        public string Url { get; set; }

        public int DisplayOrder { get; set; }
    }
}