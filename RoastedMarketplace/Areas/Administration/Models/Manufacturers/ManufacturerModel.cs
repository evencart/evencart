using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Manufacturers
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