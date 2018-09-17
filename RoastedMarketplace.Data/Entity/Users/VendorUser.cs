using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class VendorUser : FoundationEntity
    {
        public int VendorId { get; set; }

        public int UserId { get; set; }
    }
}