namespace EvenCart.Areas.Administration.Models.Settings
{
    public class TaxSettingsModel : SettingsModel
    {
        public decimal DefaultTaxRate { get; set; }

        public bool PricesIncludeTax { get; set; }

        public bool DisplayProductPricesWithoutTax { get; set; }

        public bool ChargeTaxOnShipping { get; set; }

        public bool ShippingPricesIncludeTax { get; set; }

        public int? ShippingTaxId { get; set; } = 0;
    }
}