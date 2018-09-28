using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class ProductAttributeValueModel : FoundationEntityModel, IRequiresValidations<ProductAttributeValueModel>
    {
        public string Label { get; set; }

        public string AttributeValue { get; set; }

        public decimal PriceAdjustment { get; set; }

        public void SetupValidationRules(ModelValidator<ProductAttributeValueModel> v)
        {
            v.RuleFor(x => x.AttributeValue).NotEmpty().When(x => x.Id == 0);
        }
    }
}