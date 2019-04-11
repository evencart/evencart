using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class UserCapability : FoundationEntity
    {
        public int UserId { get; set; }

        public int CapabilityId { get; set; }
    }
}