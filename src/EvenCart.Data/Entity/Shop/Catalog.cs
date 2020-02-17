using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class Catalog : FoundationEntity
    {
        public string Name { get; set; }

        public bool IsCountrySpecific { get; set; }
    }
}