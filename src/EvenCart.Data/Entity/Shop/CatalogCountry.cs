using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class CatalogCountry : FoundationEntity
    {
        public int CatalogId { get; set; }

        public int CountryId { get; set; }
    }
}