using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Addresses
{
    public class AddressService: FoundationEntityService<Address>, IAddressService
    {
        public override IEnumerable<Address> Get(Expression<Func<Address, bool>> @where, int page = 1, int count = int.MaxValue)
        {
            return Repository.Where(where)
                .Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Address, Country>())
                .SelectNested(page, count);
        }
    }
}