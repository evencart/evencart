using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Purchases
{
    public class ItemDownload : FoundationEntity
    {
        public int UserId { get; set; }

        public int DownloadId { get; set; }

        public int DownloadCount { get; set; }

        public bool Active { get; set; }
    }
}