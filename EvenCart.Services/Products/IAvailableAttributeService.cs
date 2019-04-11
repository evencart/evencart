using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface IAvailableAttributeService : IFoundationEntityService<AvailableAttribute>
    {
        IEnumerable<AvailableAttribute> GetAvailableAttributes(out int totalResults, string searchText = null, int page = 1,
            int count = int.MaxValue);
    }
}