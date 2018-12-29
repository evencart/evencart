using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IManufacturerService : IFoundationEntityService<Manufacturer>
    {
        IEnumerable<Manufacturer> SearchManufacturers(out int totalResults, string searchText, int page = 1, int count = int.MaxValue);
    }
}