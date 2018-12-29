using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class ProductsSettings : ISettingGroup
    {
        public bool DisplayProductsFromChildCategories { get; set; }
    }
}