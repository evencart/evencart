using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class ManufacturerService : FoundationEntityService<Manufacturer>, IManufacturerService
    {
        public IEnumerable<Manufacturer> SearchManufacturers(out int totalResults, string searchText, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(searchText));
            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}