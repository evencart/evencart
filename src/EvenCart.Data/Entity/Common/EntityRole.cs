using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Common
{
    public class EntityRole : FoundationEntity
    {
        public int RoleId { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }
    }
}