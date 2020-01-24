using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Common
{
    public class EntityTag : FoundationEntity
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string Tag { get; set; }
    }
}