using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class RoleCapability : FoundationEntity
    {
        public int RoleId { get; set; }

        public int CapabilityId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Capability Capability { get; set; }
    }
}