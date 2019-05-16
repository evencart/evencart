using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Reviews
{
    public class ReviewSearchModel : PublicSearchModel, IRequiresValidations<ReviewSearchModel>
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// true if only reviews with verified purchase should be returned. false otherwise.
        /// </summary>
        public bool VerifiedPurchase { get; set; }
        /// <summary>
        /// Set to a value to search reviews of that rating value
        /// </summary>
        public int? Rating { get; set; }

        public void SetupValidationRules(ModelValidator<ReviewSearchModel> v)
        {
            v.RuleFor(x => x.Page).GreaterThan(0);
            v.RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}