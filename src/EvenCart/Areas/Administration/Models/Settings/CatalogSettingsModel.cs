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

using EvenCart.Data.Enum;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class CatalogSettingsModel : SettingsModel
    {
        public bool DisplayProductsFromChildCategories { get; set; }

        public int NumberOfProductsPerPage { get; set; }

        public CatalogPaginationType CatalogPaginationType { get; set; }

        public bool DisplaySortOptions { get; set; }

        public bool EnableReviews { get; set; }

        public bool EnableReviewModeration { get; set; }

        public bool AllowReviewModification { get; set; }

        public bool AllowReviewsForStorePurchaseOnly { get; set; }

        public bool AllowOneReviewPerUserPerItem { get; set; }

        public bool EnableRelatedProducts { get; set; }

        public int NumberOfRelatedProducts { get; set; }

        public int NumberOfReviewsToDisplayOnProductPage { get; set; }

        public string DisplayNameForPrivateReviews { get; set; }

        public int NumberOfDaysForPendingReviews { get; set; }
    }
}