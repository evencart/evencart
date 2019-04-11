using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class TaxSettings : ISettingGroup
    {
        public decimal DefaultTaxRate { get; set; }

        public bool PricesIncludeTax { get; set; }

        public bool DisplayProductPricesWithoutTax { get; set; }

        public bool ChargeTaxOnShipping { get; set; }

        public bool ShippingPricesIncludeTax { get; set; }

        public int? ShippingTaxId { get; set; }
    }
}