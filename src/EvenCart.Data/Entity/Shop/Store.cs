using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class Store : FoundationEntity, ISoftDeletable
    {
        public string Name { get; set; }

        public bool Live { get; set; }

        public bool Deleted { get; set; }
    }
}