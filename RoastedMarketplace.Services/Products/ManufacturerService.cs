using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
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