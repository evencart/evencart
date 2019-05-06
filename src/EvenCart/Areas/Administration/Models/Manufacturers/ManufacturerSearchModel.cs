using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Manufacturers
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