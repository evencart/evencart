using System;
using System.Collections.Generic;
using System.Linq;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class AvailableAttributeService : FoundationEntityService<AvailableAttribute>, IAvailableAttributeService
    {
        public IEnumerable<AvailableAttribute> GetAvailableAttributes(out int totalResults, string searchText = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(searchText));

            return query.Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<AvailableAttribute, AvailableAttributeValue>())
                .OrderBy(x => x.Name)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public override AvailableAttribute Get(int id)
        {
            return Repository.Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<AvailableAttribute, AvailableAttributeValue>())
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }
    }
}