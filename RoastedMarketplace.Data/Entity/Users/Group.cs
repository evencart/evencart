using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class Group : FoundationEntity
    {
        public string Name { get; set; }

        public bool Default { get; set; }
    }
}