using System.Linq;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Taxes
{
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

        public decimal GetFinalTaxRate(Product product, Address address)
        {
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

            return tax.TaxRates.First().Rate;
        }
    }
}