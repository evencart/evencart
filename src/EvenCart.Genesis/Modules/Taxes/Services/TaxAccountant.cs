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

using System.Linq;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using Genesis;
using Genesis.Modules.Addresses;

namespace EvenCart.Services.Taxes
{
    [AutoResolvable]
    public class TaxAccountant : ITaxAccountant
    {
        private readonly ITaxService _taxService;
        private readonly ITaxRateService _taxRateService;
        private readonly TaxSettings _taxSettings;
        public TaxAccountant(ITaxRateService taxRateService, ITaxService taxService, TaxSettings taxSettings)
        {
            _taxRateService = taxRateService;
            _taxService = taxService;
            _taxSettings = taxSettings;
        }

        public decimal GetFinalTaxRate(Product product, Address address, out string taxName)
        {
            taxName = "Tax";
            if (!product.ChargeTaxes)
                return 0;

            var taxId = product.TaxId ?? 0;
            //does product have a tax id, if not, proceed for other options
            if (!product.TaxId.HasValue)
            {
                //check if the category defines a tax 
                if (!product.Categories?.Any(x => x.TaxId.HasValue) ?? true)
                {
                    return _taxSettings.DefaultTaxRate; //go with default
                }
                var categoryTaxId = product.Categories.First(x => x.TaxId.HasValue).TaxId;
                if (categoryTaxId != null)
                    taxId = categoryTaxId.Value;
            }

            if (taxId == 0)
                return _taxSettings.DefaultTaxRate;

            var tax = _taxService.GetWithTaxRate(taxId, address?.CountryId ?? 0, address?.StateProvinceId ?? 0, address?.ZipPostalCode);
            if (tax?.TaxRates == null || !tax.TaxRates.Any())
                return _taxSettings.DefaultTaxRate;
            taxName = tax.Name;
            return tax.TaxRates.First().Rate;
        }
    }
}