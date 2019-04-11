using EvenCart.Data.Entity.Reviews;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Reviews;

namespace EvenCart.Factories.Reviews
{
    public interface IReviewModelFactory : IModelFactory<Review, ReviewModel>
    {
        
    }
}