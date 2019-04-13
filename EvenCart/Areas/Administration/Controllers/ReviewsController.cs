using System.Linq;
using EvenCart.Areas.Administration.Factories.Reviews;
using EvenCart.Areas.Administration.Models.Reviews;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class ReviewsController : FoundationAdminController
    {
        private readonly IReviewService _reviewService;
        private readonly IReviewModelFactory _reviewModelFactory;
        public ReviewsController(IReviewService reviewService, IReviewModelFactory reviewModelFactory)
        {
            _reviewService = reviewService;
            _reviewModelFactory = reviewModelFactory;
        }

        [DualGet("", Name = AdminRouteNames.ReviewsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageReviews)]
        public IActionResult ReviewsList(ReviewSearchModel reviewSearchModel)
        {
            reviewSearchModel = reviewSearchModel ?? new ReviewSearchModel()
            {
                Current = 1,
                RowCount = 15
            };
            var reviews = _reviewService.GetReviews(out var totalResults, reviewSearchModel.SearchPhrase, reviewSearchModel.ProductSearch,
                reviewSearchModel.Published, reviewSearchModel.ProductId, reviewSearchModel.Current,
                reviewSearchModel.RowCount);

            var reviewModels = reviews.Select(_reviewModelFactory.Create).ToList();
            return R.Success.With("reviews", reviewModels)
                .WithGridResponse(totalResults, reviewSearchModel.Current, reviewModels.Count)
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveReview, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageReviews)]
        [ValidateModelState(ModelType = typeof(ReviewModel))]
        public IActionResult SaveReview(ReviewModel reviewModel)
        {
            var review = reviewModel.Id > 0 ? _reviewService.Get(reviewModel.Id) : null;
            if (review == null)
                return NotFound();
           
            review.Title = reviewModel.Title;
            review.Description = reviewModel.Description;
            review.Published = reviewModel.Published;
            review.Private = reviewModel.Private;
            review.Rating = reviewModel.Rating;

            if (review.Id == 0)
                review.CreatedOn = reviewModel.CreatedOn;

            _reviewService.InsertOrUpdate(review);
            return R.Success.Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteReview)]
        [CapabilityRequired(CapabilitySystemNames.ManageReviews)]
        public IActionResult DeleteReview(int reviewId)
        {
            Review review;
            if (reviewId <= 0 || (review = _reviewService.Get(reviewId)) == null)
                return NotFound();

            //delete the review
            _reviewService.Delete(review);
            return R.Success.Result;
        }

        [DualGet("{reviewId}", Name = AdminRouteNames.GetReview)]
        [CapabilityRequired(CapabilitySystemNames.ManageReviews)]
        public IActionResult ReviewEditor(int reviewId)
        {
            var review = reviewId > 0 ? _reviewService.Get(reviewId) : new Review();
            if (review == null)
                return NotFound();

            var reviewModel = _reviewModelFactory.Create(review);
            return R.Success.With("review", reviewModel).Result;
        }

        
    }
}