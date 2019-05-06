using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Reviews
{
    public class ReviewSearchModel : PublicSearchModel, IRequiresValidations<ReviewSearchModel>
    {
        public int ProductId { get; set; }

        public bool VerifiedPurchase { get; set; }

        public int? Rating { get; set; }

        public void SetupValidationRules(ModelValidator<ReviewSearchModel> v)
        {
            v.RuleFor(x => x.Page).GreaterThan(0);
            v.RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}