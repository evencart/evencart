using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.MediaEntities
{
    public class EntityMedia : FoundationEntity
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int MediaId { get; set; }
    }
}
