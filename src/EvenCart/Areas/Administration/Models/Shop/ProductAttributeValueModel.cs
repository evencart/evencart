using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
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