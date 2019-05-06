using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Promotions
{
    public class DiscountSearchModel : AdminSearchModel, IRequiresValidations<DiscountSearchModel>
    {
        public void SetupValidationRules(ModelValidator<DiscountSearchModel> v)
        {
            v.RuleFor(x => x.RowCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            v.RuleFor(x => x.Current).GreaterThanOrEqualTo(1);
        }
    }
}