using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class Download : FoundationEntity
    {
        public string Guid { get; set; }

        public int ProductId { get; set; }

        public int ProductVariantId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FileLocation { get; set; }

        public bool IsFileLocationUrl { get; set; }

        public byte[] FileBytes { get; set; }

        public bool RequirePurchase { get; set; }

        public bool RequireLogin { get; set; }

        public string FileType { get; set; }

        public int MaximumDownloads { get; set; }

        public int DisplayOrder { get; set; }

        public DownloadActivationType DownloadActivationType { get; set; }

        public bool Published { get; set; }

        public string FileExtension { get; set; }
    }
}