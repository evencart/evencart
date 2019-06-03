using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Addresses
{
    public class AddressService: FoundationEntityService<Address>, IAddressService
    {
        public override IEnumerable<Address> Get(Expression<Func<Address, bool>> @where, int page = 1, int count = int.MaxValue)
        {
            return Repository.Where(where)
                .Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Join<StateOrProvince>("StateProvinceId", "Id", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Address, Country>())
                .Relate(RelationTypes.OneToOne<Address, StateOrProvince>())
                .SelectNested(page, count);
        }

        public Address Get<T>(int id)
        {
            var entityName = typeof(T).Name;
            return FirstOrDefault(x => x.EntityName == entityName && x.Id == id);
        }
    }
}