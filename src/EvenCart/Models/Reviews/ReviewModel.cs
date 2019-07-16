using System;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using EvenCart.Models.Products;
using FluentValidation;

namespace EvenCart.Models.Reviews
{
    public class ReviewModel : FoundationEntityModel, IRequiresValidations<ReviewModel>
    {
        /// <summary>
        /// The rating value between 1 and 5
        /// </summary>
        public int Rating { get; set; }

        public decimal RatingPercent => (decimal) Rating / 5 * 100;
        /// <summary>
        /// The title of the review
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the review
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies if the review was posted by the user who bought from the store. Ignored for POST requests.
        /// </summary>
        public bool VerifiedPurchase { get; set; }
        /// <summary>
        /// The creation date of review. Ignored for POST requests.
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The creation date of review in user's timezone
        /// </summary>
        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();
        /// <summary>
        /// Specifies if user information should be kept private or public
        /// </summary>
        public bool Private { get; set; }
        /// <summary>
        /// The display name of user for review. Ignored for POST requests.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The id of product for which review is being posted
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The id of order for which review is being posted
        /// </summary>
        public int OrderId { get; set; }

        public virtual ProductModel Product { get; set; }
        public void SetupValidationRules(ModelValidator<ReviewModel> v)
        {
            v.RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
            v.RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}