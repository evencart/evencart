using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class UserGroup : FoundationEntity
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }
    }
}