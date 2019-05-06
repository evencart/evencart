using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IManufacturerService : IFoundationEntityService<Manufacturer>
    {
        IEnumerable<Manufacturer> SearchManufacturers(out int totalResults, string searchText, int page = 1, int count = int.MaxValue);
    }
}