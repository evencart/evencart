using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Taxes;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Taxes
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