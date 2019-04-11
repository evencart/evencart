using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Taxes;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Taxes
{
    public class TaxRateService : FoundationEntityService<TaxRate>, ITaxRateService
    {
        public override IEnumerable<TaxRate> Get(Expression<Func<TaxRate, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Join<StateOrProvince>("StateOrProvinceId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<TaxRate, Country>())
                .Relate(RelationTypes.OneToOne<TaxRate, StateOrProvince>())
                .Where(where)
                .OrderBy(x => x.Id)
                .SelectNested(page, count);
        }

        public override TaxRate Get(int id)
        {
            return Get(x => x.Id == id).FirstOrDefault();
        }
    }
}