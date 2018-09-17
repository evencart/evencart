using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class Role : FoundationEntity
    {
        public string RoleName { get; set; }

        public bool IsSystemRole { get; set; }

        public string SystemName { get; set; }

        public bool IsActive { get; set; }
    }
}