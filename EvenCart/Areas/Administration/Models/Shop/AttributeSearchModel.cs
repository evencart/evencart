using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class AttributeSearchModel : AdminSearchModel, IRequiresValidations<AttributeSearchModel>
    {
        public void SetupValidationRules(ModelValidator<AttributeSearchModel> v)
        {
            v.RuleFor(x => x.RowCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            v.RuleFor(x => x.Current).GreaterThanOrEqualTo(1);
        }
    }
}