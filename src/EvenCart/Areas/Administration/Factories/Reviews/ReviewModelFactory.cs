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

using EvenCart.Areas.Administration.Models.Reviews;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Factories.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Reviews
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
                    LocalizationHelper.Localize("Store Customer", ApplicationEngine.CurrentLanguage.CultureCode);
            }

            if (review.Product != null)
                model.ProductName = review.Product.Name;

            if (review.User != null)
                model.UserName = review.User.Name;
            return model;
        }
    }
}