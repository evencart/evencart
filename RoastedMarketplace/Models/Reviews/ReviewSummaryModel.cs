using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Reviews
{
    public class ReviewSummaryModel : FoundationModel
    {
        public decimal AverageRating { get; set; }

        public int TotalReviews { get; set; }

        public int TotalRatings { get; set; }

        public decimal PositivePercent => (AverageRating / 5) * 100;
    }
}