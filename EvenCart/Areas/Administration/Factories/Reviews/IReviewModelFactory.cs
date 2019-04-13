using EvenCart.Areas.Administration.Models.Reviews;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Reviews
{
    public interface IReviewModelFactory : IModelFactory<Review, ReviewModel>
    {
        
    }
}