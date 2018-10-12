using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class CategorySearchModel : SearchModel, IRequiresValidations<CategorySearchModel>
    {
        public void SetupValidationRules(ModelValidator<CategorySearchModel> v)
        {
            v.RuleFor(x => x.RowCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            v.RuleFor(x => x.Current).GreaterThanOrEqualTo(1);
        }
    }
}