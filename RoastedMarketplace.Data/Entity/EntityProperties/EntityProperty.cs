using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.EntityProperties
{
    public class EntityProperty : FoundationEntity
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}