#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using Genesis.Services;
using EvenCart.Data.Entity.Shop;
using Genesis.Extensions;
using Genesis.Modules.Addresses;

namespace EvenCart.Services.Products
{
    public class WarehouseService : GenesisEntityService<Warehouse>, IWarehouseService
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