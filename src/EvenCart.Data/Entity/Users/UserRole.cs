using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class UserRole : FoundationEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }

}