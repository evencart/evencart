using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Manufacturers
{
    public class ManufacturerSearchModel : AdminSearchModel, IRequiresValidations<ManufacturerSearchModel>
    {
        public void SetupValidationRules(ModelValidator<ManufacturerSearchModel> v)
        {
            v.RuleFor(x => x.RowCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            v.RuleFor(x => x.Current).GreaterThanOrEqualTo(1);
        }
    }
}