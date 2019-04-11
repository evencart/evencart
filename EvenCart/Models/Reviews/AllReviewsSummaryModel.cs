using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Reviews
{
    public class AllReviewsSummaryModel : FoundationModel
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