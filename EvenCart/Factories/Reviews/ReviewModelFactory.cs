using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Factories.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Reviews;

namespace EvenCart.Factories.Reviews
{
    public class ReviewModelFactory : IReviewModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly CatalogSettings _catalogSettings;
        private readonly IProductModelFactory _productModelFactory;
        public ReviewModelFactory(IModelMapper modelMapper, CatalogSettings catalogSettings, IProductModelFactory productModelFactory)
        {
            _modelMapper = modelMapper;
            _catalogSettings = catalogSettings;
            _productModelFactory = productModelFactory;
        }

        public ReviewModel Create(Review review)
        {
            var model = _modelMapper.Map<ReviewModel>(review);
            model.DisplayName = review.Private ? _catalogSettings.DisplayNameForPrivateReviews : review.User?.Name;
            if (model.DisplayName.IsNullEmptyOrWhiteSpace())
            {
                model.DisplayName =
                    LocalizationHelper.Localize("Store Customer", ApplicationEngine.CurrentLanguageCultureCode);
            }

            if (review.Product != null)
                model.Product = _productModelFactory.Create(review.Product);
            return model;
        }
    }
}