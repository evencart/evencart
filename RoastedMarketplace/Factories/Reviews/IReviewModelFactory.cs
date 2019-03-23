using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Reviews;

namespace RoastedMarketplace.Factories.Reviews
{
    public interface IReviewModelFactory : IModelFactory<Review, ReviewModel>
    {
        
    }
}