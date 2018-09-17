using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class UserCapability : FoundationEntity
    {
        public int UserId { get; set; }

        public int CapabilityId { get; set; }
    }
}