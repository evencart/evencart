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
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Taxes;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Taxes
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