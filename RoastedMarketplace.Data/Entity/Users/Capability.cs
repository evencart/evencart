using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public partial class Capability : FoundationEntity
    {
        public string CapabilityName { get; set; }

        public bool IsActive { get; set; }
    }
}