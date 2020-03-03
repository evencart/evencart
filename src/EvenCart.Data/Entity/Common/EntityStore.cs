using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Common
{
    public class EntityStore : FoundationEntity
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int StoreId { get; set; }
    }
}