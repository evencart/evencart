using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class ProductSpecificationValueModel : FoundationEntityModel, IRequiresValidations<ProductSpecificationValueModel>
    {
        public string Label { get; set; }

        public string AttributeValue { get; set; }

        public void SetupValidationRules(ModelValidator<ProductSpecificationValueModel> v)
        {
            v.RuleFor(x => x.AttributeValue).NotEmpty().When(x => x.Id == 0);
        }
    }
}