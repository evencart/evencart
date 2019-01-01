using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class CatalogSettingsModel : SettingsModel
    {
        public bool DisplayProductsFromChildCategories { get; set; }

        public int NumberOfProductsPerPage { get; set; }

        public CatalogPaginationType CatalogPaginationType { get; set; }

        public bool DisplaySortOptions { get; set; }
        
    }
}