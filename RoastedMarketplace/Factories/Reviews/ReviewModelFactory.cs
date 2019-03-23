using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Factories.Products;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Reviews;

namespace RoastedMarketplace.Factories.Reviews
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