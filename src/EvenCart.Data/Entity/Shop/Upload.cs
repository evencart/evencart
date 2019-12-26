using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class Upload : FoundationEntity
    {
        public string Guid { get; set; }

        public int UserId { get; set; }

        public byte[] FileBytes { get; set; }

        public string FileType { get; set; }

        public string FileExtension { get; set; }
    }
}