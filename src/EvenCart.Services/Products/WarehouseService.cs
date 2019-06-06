using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class WarehouseService : FoundationEntityService<Warehouse>, IWarehouseService
    {
        public override Warehouse Get(int id)
        {
            return GetWithJoin()
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }

        public override IEnumerable<Warehouse> Get(out int totalResults, Expression<Func<Warehouse, bool>> @where, Expression<Func<Warehouse, object>> orderBy = null,
            RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            if (orderBy == null)
                orderBy = x => x.Id;
            return GetWithJoin()
                .Where(where)
                .OrderBy(orderBy)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public override IEnumerable<Warehouse> Get(Expression<Func<Warehouse, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return GetWithJoin()
                .Where(where)
                .OrderBy(x => x.DisplayOrder)
                .SelectNested(page, count);
        }

        private IEntitySet<Warehouse> GetWithJoin()
        {
            return Repository.Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Warehouse, Address>())
                .Relate<Country>((warehouse, country) => { warehouse.Address.Country = country; });
        }
    }
}