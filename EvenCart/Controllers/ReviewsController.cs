using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using EvenCart.Services.Reviews;
using EvenCart.Factories.Products;
using EvenCart.Factories.Reviews;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class ReviewsController : FoundationController
    {
        private readonly IReviewService _reviewService;
        private readonly IOrderService _orderService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IModelMapper _modelMapper;
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IReviewModelFactory _reviewModelFactory;
        public ReviewsController(IReviewService reviewService, IOrderService orderService, CatalogSettings catalogSettings, IModelMapper modelMapper, IProductService productService, IProductModelFactory productModelFactory, IReviewModelFactory reviewModelFactory)
        {
            _reviewService = reviewService;
            _orderService = orderService;
            _catalogSettings = catalogSettings;
            _modelMapper = modelMapper;
            _productService = productService;
            _productModelFactory = productModelFactory;
            _reviewModelFactory = reviewModelFactory;
        }

        [ValidateModelState(ModelType = typeof(ReviewModel))]
        [DualPost("reviews", Name = RouteNames.SaveUserReview, OnlyApi = true)]
        [Authorize]
        public IActionResult SaveReview(ReviewModel reviewModel)
        {
            if (!_catalogSettings.EnableReviews)
                return NotFound();

            var userId = ApplicationEngine.CurrentUser.Id;
            var verifiedPurchase = false;
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
                    return R.Fail.With("error", T("Invalid product details provided")).Result;

            }

            if (reviewModel.Id > 0)
            {
                if (!_catalogSettings.AllowReviewModification)
                    return R.Fail.With("error", T("Review modification is not allowed")).Result;
                //is that the same user saving the review?
                var savedReview = _reviewService.Get(reviewModel.Id);
                if (savedReview == null || savedReview.UserId != CurrentUser.Id)
                {
                    return NotFound();
                }
            }
            else if (_catalogSettings.AllowOneReviewPerUserPerItem && reviewModel.Id == 0)
            {
                //check if user has already reviewed this product
                var savedReview =
                    _reviewService.FirstOrDefault(x => x.ProductId == reviewModel.ProductId && x.UserId == CurrentUser.Id);
                if (savedReview != null)
                {
                    return R.Fail.With("error", T("The product has been already reviewed")).Result;
                }
            }
            //create a new review
            var review = _modelMapper.Map<Review>(reviewModel);
            review.CreatedOn = DateTime.UtcNow;
            review.UpdatedOn = DateTime.UtcNow;
            review.VerifiedPurchase = verifiedPurchase;
            review.Published = !_catalogSettings.EnableReviewModeration;
            review.UserId = userId;
            _reviewService.InsertOrUpdate(review);
            return R.Success.Result;
        }

        [ValidateModelState(ModelType = typeof(ReviewModel))]
        [DualPost("reviews/{reviewId}", Name = RouteNames.DeleteUserReview, OnlyApi = true)]
        [Authorize]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_catalogSettings.EnableReviews)
                return NotFound();

            if (reviewId > 0)
            {
                if (!_catalogSettings.AllowReviewModification)
                    return R.Fail.With("error", T("Review deletion is not allowed")).Result;
                //is that the same user saving the review?
                var savedReview = _reviewService.Get(reviewId);
                if (savedReview == null || savedReview.UserId != CurrentUser.Id)
                {
                    return NotFound();
                }
                _reviewService.Delete(savedReview);
            }
            return R.Success.Result;
        }
        [HttpGet("reviews/{productId}/{reviewId}", Name = RouteNames.ReviewEditor)]
        [Authorize]
        public IActionResult ReviewEditor(int productId, int reviewId)
        {
            //check if the product is valid
            var product = _productService.Get(productId);
            if (!product.IsPublic())
                return NotFound();
            var currentUser = ApplicationEngine.CurrentUser;
            var review = reviewId > 0 ? _reviewService.Get(reviewId) : new Review()
            {
                ProductId =  productId,
                UserId = currentUser.Id
            };
            if (review == null || review.ProductId != productId)
                return NotFound();
            if (review.UserId != ApplicationEngine.CurrentUser.Id || !_catalogSettings.AllowReviewModification)
                return NotFound();

            var response = R.Success;
            //check if review should be allowed for this product
            if (_catalogSettings.AllowReviewsForStorePurchaseOnly)
            {
                //check if user has purchased anything here
                var orders = _orderService.GetOrders(out int _, userId: currentUser.Id,
                    paymentStatus: new List<PaymentStatus>() {PaymentStatus.Complete},
                    orderStatus: new List<OrderStatus>() {OrderStatus.Complete});
                if (orders.SelectMany(x => x.OrderItems).All(y => y.ProductId != productId))
                {
                    return NotFound();
                }
            }

            if (_catalogSettings.AllowOneReviewPerUserPerItem && reviewId == 0)
            {
                //check if user has already reviewed this product
                var savedReview =
                    _reviewService.FirstOrDefault(x => x.ProductId == productId && x.UserId == CurrentUser.Id);
                if (savedReview != null)
                {
                    return RedirectToRoute(RouteNames.AccountReviews);
                }
            }

            var reviewModel = _reviewModelFactory.Create(review);
            var productModel = _productModelFactory.Create(product);

            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Review Center", RouteNames.AccountReviews);
            SetBreadcrumbToRoute("Edit Review", RouteNames.ReviewEditor);
            return response.With("review", reviewModel).With("product", productModel).Result;
        }

        [DynamicRoute(Name = RouteNames.UserReviewsList, SeoEntityName = nameof(Product), SettingName = nameof(UrlSettings.ProductUrlTemplate), TemplateSuffix = "/reviews", ParameterName = nameof(ReviewSearchModel.ProductId))]
        public IActionResult ReviewsList(ReviewSearchModel reviewSearchModel)
        {
            return ReviewsListApi(reviewSearchModel);
        }

        [DualGet("reviews/{productId}", Name = RouteNames.UserReviewsList, OnlyApi = true)]
        public IActionResult ReviewsListApi(ReviewSearchModel reviewSearchModel)
        {
            //check if the product is valid
            var product = _productService.Get(reviewSearchModel.ProductId);
            if (!product.IsPublic())
                return NotFound();

            IList<Review> reviews;
            var ratings = reviewSearchModel.Rating.HasValue
                ? new List<int>() {reviewSearchModel.Rating.Value}
                : new List<int>() { 1, 2, 3, 4, 5 };
            int totalMatches;
            if (reviewSearchModel.VerifiedPurchase)
                reviews = _reviewService.Get(out totalMatches,
                    x => x.ProductId == reviewSearchModel.ProductId && ratings.Contains(x.Rating) &&
                         x.VerifiedPurchase == true && x.Published, page: reviewSearchModel.Page,
                    count: reviewSearchModel.Count).ToList();
            else
                reviews = _reviewService.Get(out totalMatches,
                    x => x.ProductId == reviewSearchModel.ProductId && ratings.Contains(x.Rating) && x.Published,
                    page: reviewSearchModel.Page,
                    count: reviewSearchModel.Count).ToList();

            var reviewModels = reviews.Select(_reviewModelFactory.Create).ToList();

            var reviewsSummaryModel = new AllReviewsSummaryModel()
            {
                TotalReviews = totalMatches
            };
            //find best and worst review
            ReviewModel bestReview = null, worstReview = null;
            if (reviews.Count > 1 && reviews.Count < reviewSearchModel.Count)
            {
                bestReview = reviewModels.OrderByDescending(x => x.Rating).First();
                worstReview = reviewModels.OrderBy(x => x.Rating).First();
                reviewsSummaryModel.FiveStarCount = reviews.Count(x => x.Rating == 5);
                reviewsSummaryModel.FourStarCount = reviews.Count(x => x.Rating == 4);
                reviewsSummaryModel.ThreeStarCount = reviews.Count(x => x.Rating == 3);
                reviewsSummaryModel.TwoStarCount = reviews.Count(x => x.Rating == 2);
                reviewsSummaryModel.OneStarCount = reviews.Count(x => x.Rating == 1);
            }
            else
            {
                if (reviews.Count > 1)
                {
                    bestReview = _reviewModelFactory.Create(_reviewService.GetBestReview(product.Id));
                    worstReview = _reviewModelFactory.Create(_reviewService.GetWorstReview(product.Id));
                    reviewsSummaryModel.FiveStarCount = _reviewService.Count(x => x.Rating == 5 && x.ProductId == product.Id && x.Published);
                    reviewsSummaryModel.FourStarCount = _reviewService.Count(x => x.Rating == 4 && x.ProductId == product.Id && x.Published);
                    reviewsSummaryModel.ThreeStarCount = _reviewService.Count(x => x.Rating == 3 && x.ProductId == product.Id && x.Published);
                    reviewsSummaryModel.TwoStarCount = _reviewService.Count(x => x.Rating == 2 && x.ProductId == product.Id && x.Published);
                    reviewsSummaryModel.OneStarCount = _reviewService.Count(x => x.Rating == 1 && x.ProductId == product.Id && x.Published);
                }
            }

            var productModel = _productModelFactory.Create(product);

            //breadcrumbs
            //set breadcrumb nodes
            SetBreadcrumbToRoute(product.Name, RouteNames.SingleProduct,
                new {seName = product.SeoMeta.Slug, id = product.Id}, localize: false);
            SetBreadcrumbToUrl("Reviews", "");

            return R.Success
                .With("product", productModel)
                .With("reviews", reviewModels)
                .With("bestReview", bestReview)
                .With("worstReview", worstReview)
                .WithGridResponse(totalMatches, reviewSearchModel.Page, reviewSearchModel.Count)
                .With("summary", reviewsSummaryModel).Result;
        }

        [DualGet("~/account/reviews", Name = RouteNames.AccountReviews)]
        [Authorize]
        public IActionResult AccountReviews(ReviewSearchModel reviewSearchModel)
        {
            var ratings = reviewSearchModel.Rating.HasValue
                ? new List<int>() { reviewSearchModel.Rating.Value }
                : new List<int>() { 1, 2, 3, 4, 5 };

            var reviews = _reviewService.Get(out var totalMatches,
                x => x.UserId == CurrentUser.Id && ratings.Contains(x.Rating) && x.Published, page: reviewSearchModel.Page, count: reviewSearchModel.Count).ToList();

            var reviewModels = reviews.Select(_reviewModelFactory.Create).ToList();

            var reviewsSummaryModel = new AllReviewsSummaryModel()
            {
                TotalReviews = totalMatches
            };
            //find best and worst review
            if (reviews.Count > 1 && reviews.Count < reviewSearchModel.Count)
            {
                reviewsSummaryModel.FiveStarCount = reviews.Count(x => x.Rating == 5);
                reviewsSummaryModel.FourStarCount = reviews.Count(x => x.Rating == 4);
                reviewsSummaryModel.ThreeStarCount = reviews.Count(x => x.Rating == 3);
                reviewsSummaryModel.TwoStarCount = reviews.Count(x => x.Rating == 2);
                reviewsSummaryModel.OneStarCount = reviews.Count(x => x.Rating == 1);
            }
            else
            {
                if (reviews.Count > 1)
                {
                    reviewsSummaryModel.FiveStarCount = _reviewService.Count(x => x.Rating == 5 && x.UserId == CurrentUser.Id && x.Published);
                    reviewsSummaryModel.FourStarCount = _reviewService.Count(x => x.Rating == 4 && x.UserId == CurrentUser.Id && x.Published);
                    reviewsSummaryModel.ThreeStarCount = _reviewService.Count(x => x.Rating == 3 && x.UserId == CurrentUser.Id && x.Published);
                    reviewsSummaryModel.TwoStarCount = _reviewService.Count(x => x.Rating == 2 && x.UserId == CurrentUser.Id && x.Published);
                    reviewsSummaryModel.OneStarCount = _reviewService.Count(x => x.Rating == 1 && x.UserId == CurrentUser.Id && x.Published);
                }
            }
            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Review Center", RouteNames.AccountReviews);

            return R.Success
                .With("reviews", reviewModels)
                .WithGridResponse(totalMatches, reviewSearchModel.Page, reviewSearchModel.Count)
                .With("summary", reviewsSummaryModel).Result;
        }

        [DualGet("reviews/user/pending", Name = RouteNames.UserPendingReviewsList, OnlyApi = true)]
        [Authorize]
        public IActionResult UserPendingReviewsListApi()
        {
            var allowedOrderStatus = new List<OrderStatus>()
            {
                OrderStatus.Complete, OrderStatus.Returned
            };
            var allowedPaymentStatus = new List<PaymentStatus>()
            {
                PaymentStatus.Complete, PaymentStatus.Refunded
            };
            //get the user's orders
            var orders = _orderService.GetOrders(out int _, userId: CurrentUser.Id, orderStatus: allowedOrderStatus,
                paymentStatus: allowedPaymentStatus);
            var orderItems = orders.SelectMany(x => x.OrderItems);
            var reviewedOrders = _reviewService.Get(x => x.UserId == CurrentUser.Id).ToList();
            var pendingReviews = new List<PendingReviewModel>();
            foreach (var orderItem in orderItems)
            {
                if (reviewedOrders.Any(x => x.OrderId == orderItem.OrderId && x.ProductId == orderItem.ProductId))
                    continue;
                var pendingReviewModel = new PendingReviewModel()
                {
                    OrderGuid = orderItem.Order.Guid,
                    OrderNumber = orderItem.Order.OrderNumber,
                    OrderStatus = orderItem.Order.OrderStatus,
                    Product = _productModelFactory.Create(orderItem.Product)
                };
                pendingReviews.Add(pendingReviewModel);
            }
            return R.Success.With("pendingReviews", pendingReviews).Result;
        }
    }
}