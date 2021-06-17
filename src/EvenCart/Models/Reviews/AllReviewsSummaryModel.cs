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

using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Reviews
{
    public class AllReviewsSummaryModel : GenesisModel
    {
        public int FiveStarCount { get; set; }

        public int FourStarCount { get; set; }

        public int ThreeStarCount { get; set; }

        public int TwoStarCount { get; set; }

        public int OneStarCount { get; set; }

        public decimal FiveStarPercent => decimal.Round(TotalReviews == 0 ? 0 : (decimal)FiveStarCount * 100 / TotalReviews, 2);

        public decimal FourStarPercent => decimal.Round(TotalReviews == 0 ? 0 : (decimal)FourStarCount * 100 / TotalReviews, 2);

        public decimal ThreeStarPercent => decimal.Round(TotalReviews == 0 ? 0 : (decimal)ThreeStarCount * 100 / TotalReviews, 2);

        public decimal TwoStarPercent => decimal.Round(TotalReviews == 0 ? 0 : (decimal)TwoStarCount * 100 / TotalReviews, 2);

        public decimal OneStarPercent => decimal.Round(TotalReviews == 0 ? 0 : (decimal)OneStarCount * 100 / TotalReviews, 2);

        public int TotalReviews { get; set; }
    }
}