using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Reviews;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Reviews;

namespace RoastedMarketplace.Controllers
{
    public class ReviewsController : FoundationController
    {
        private readonly IReviewService _reviewService;
        private readonly IOrderService _orderService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IModelMapper _modelMapper;
        private readonly IProductService _productService;
        public ReviewsController(IReviewService reviewService, IOrderService orderService, CatalogSettings catalogSettings, IModelMapper modelMapper, IProductService productService)
        {
            _reviewService = reviewService;
            _orderService = orderService;
            _catalogSettings = catalogSettings;
            _modelMapper = modelMapper;
            _productService = productService;
        }

        [ValidateModelState(ModelType = typeof(ReviewModel))]
        [DualPost("reviews", Name = RouteNames.SaveReview, OnlyApi = true)]
        public IActionResult SaveReview(ReviewModel reviewModel)
        {
            var userId = ApplicationEngine.CurrentUser.Id;
            bool verifiedPurchase = false;
            if (_catalogSettings.AllowReviewsForStorePurchaseOnly)
            {
                //do we have a valid order?
                var order = _orderService.Get(reviewModel.OrderId);
                if (order == null || order.OrderItems.All(x => x.ProductId != reviewModel.ProductId))
                    return R.Fail.With("error", T("Invalid order details provided")).Result;

                if (order.OrderStatus != OrderStatus.Complete)
                    return R.Fail.With("error", T("The order is not available for review")).Result;
                //do we already have a review for this order?
                var savedReview = _reviewService.FirstOrDefault(
                    x => x.OrderId == reviewModel.OrderId && x.ProductId == reviewModel.ProductId &&
                         x.UserId == userId);

                if (savedReview != null)
                {
                    return R.Fail.With("error", T("You've already reviewed this product item")).Result;
                }

                verifiedPurchase = true;

            }
            else
            {
                //is this a valid product?
                var product = _productService.Get(reviewModel.ProductId);
                if (!product.IsPublic())
                    return R.Fail.With("error", T("Invalid order details provided")).Result;

            }
            //create a new review
            var review = _modelMapper.Map<Review>(reviewModel);
            review.CreatedOn = DateTime.UtcNow;
            review.UpdatedOn = DateTime.UtcNow;
            review.VerifiedPurchase = verifiedPurchase;
            review.Published = !_catalogSettings.EnableReviewModeration;
            review.UserId = userId;
            _reviewService.Insert(review);
            return R.Success.Result;
        }

        [DynamicRoute(Name = RouteNames.ReviewsList)]
        public IActionResult ReviewsList(ReviewSearchModel reviewSearchModel)
        {
            return ReviewsListApi(reviewSearchModel);
        }

        [DualGet("reviews/{productId}", Name = RouteNames.ReviewsList, OnlyApi = true)]
        public IActionResult ReviewsListApi(ReviewSearchModel reviewSearchModel)
        {
            //check if the product is valid
            var product = _productService.Get(reviewSearchModel.ProductId);
            if (!product.IsPublic())
                return NotFound();

            IList<Review> reviews;
            if (reviewSearchModel.VerifiedPurchase)
                reviews =_reviewService.Get(x => x.ProductId == reviewSearchModel.ProductId && x.VerifiedPurchase == true, reviewSearchModel.Page,
                    reviewSearchModel.Count).ToList();
            else
                reviews = _reviewService.Get(x => x.ProductId == reviewSearchModel.ProductId, reviewSearchModel.Page,
                    reviewSearchModel.Count).ToList();

            var reviewModels = reviews.Select(x =>
                {
                    var model = _modelMapper.Map<ReviewModel>(x);
                    model.DisplayName = x.User.Name;
                    return model;
                })
                .ToList();
            return R.Success.With("reviews", reviewModels).Result;
        }
    }
}