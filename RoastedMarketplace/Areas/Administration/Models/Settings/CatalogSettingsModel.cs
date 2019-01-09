using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class CatalogSettingsModel : SettingsModel
    {
        public bool DisplayProductsFromChildCategories { get; set; }

        public int NumberOfProductsPerPage { get; set; }

        public CatalogPaginationType CatalogPaginationType { get; set; }

        public bool DisplaySortOptions { get; set; }

        public bool EnableReviews { get; set; }

        public bool EnableReviewModeration { get; set; }

        public bool AllowReviewsForStorePurchaseOnly { get; set; }

        public bool AllowOneReviewPerUserPerItem { get; set; }

        public bool EnableRelatedProducts { get; set; }

        public int NumberOfRelatedProducts { get; set; }
    }
}