using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface IAvailableAttributeService : IFoundationEntityService<AvailableAttribute>
    {
        IEnumerable<AvailableAttribute> GetAvailableAttributes(out int totalResults, string searchText = null, int page = 1,
            int count = int.MaxValue);
    }
}