using System;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Taxes;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Taxes
{
    public class TaxService : FoundationEntityService<Tax>, ITaxService
    {
        public Tax GetWithTaxRate(int taxId, int countryId, int stateProvinceId, string zipOrPostalCode)
        {
            Expression<Func<TaxRate, bool>> where = rate => rate.CountryId == countryId;


            var tax = Repository.Where(x => x.Id == taxId)
                .Join<TaxRate>("Id", "TaxId", joinType: JoinType.LeftOuter)
                .Where(where)
                .Relate(RelationTypes.OneToMany<Tax, TaxRate>())
                .SelectNested()
                .FirstOrDefault();

            if (tax == null)
                return null;
            //filter by state and zip
            var taxRates = tax.TaxRates.Where(x => x.StateOrProvinceId == stateProvinceId &&
                                                   x.ZipOrPostalCode == zipOrPostalCode)
                .ToList();
            if (taxRates.Any())
            {
                tax.TaxRates = taxRates;
                return tax;
            }
            //filter by state only
            taxRates = tax.TaxRates.Where(x => x.StateOrProvinceId == stateProvinceId).ToList();
            if (taxRates.Any())
            {
                tax.TaxRates = taxRates;
                return tax;
            }
            return tax;
        }
    }
}