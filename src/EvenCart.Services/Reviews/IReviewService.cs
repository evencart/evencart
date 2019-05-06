using System.Collections;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Reviews;

namespace EvenCart.Services.Reviews
{
    public interface IReviewService : IFoundationEntityService<Review>
    {
        Review GetBestReview(int productId);

        Review GetWorstReview(int productId);

        IEnumerable<Review> GetReviews(out int totalResults, string reviewSearch = null, string productSearch = null, bool? publishStatus = null, int productId = 0,
            int page = 1, int count = int.MaxValue);


    }
}