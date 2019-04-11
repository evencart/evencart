using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Manufacturers
{
    public class ManufacturerModel : FoundationEntityModel, IRequiresValidations<ManufacturerModel>
    {
        public string Name { get; set; }
        public void SetupValidationRules(ModelValidator<ManufacturerModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}