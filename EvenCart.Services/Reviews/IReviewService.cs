using EvenCart.Core.Services;
using EvenCart.Data.Entity.Reviews;

namespace EvenCart.Services.Reviews
{
    public interface IReviewService : IFoundationEntityService<Review>
    {
        Review GetBestReview(int productId);

        Review GetWorstReview(int productId);
    }
}