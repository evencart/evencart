using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductAttributeModel : FoundationEntityModel, IRequiresValidations<ProductAttributeModel>
    {
        public string Name { get; set; }

        public InputFieldType InputFieldType { get; set; }

        public int InputFieldTypeId => (int) InputFieldType;

        public string InputFieldTypeDisplay => InputFieldType.ToString();

        public string Label { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; }

        public List<ProductAttributeValueModel> Values { get; set; } = new List<ProductAttributeValueModel>();

        public List<SelectListItem> ValuesAsSelectListItems => SelectListHelper.GetSelectItemList(Values, x => x.Id,
            x => x.AttributeValue);

        public void SetupValidationRules(ModelValidator<ProductAttributeModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty().When(x => x.Id == 0);
            //v.RuleForEach(x => x.Values).SetValidator(new ModelValidator<ProductAttributeValueModel>());
            v.RuleFor(x => x.Values).Custom((list, context) =>
            {
                if (!list.Any())
                {
                    context.AddFailure(nameof(ProductAttributeValueModel.AttributeValue), "At least one attribute value must be provided");
                }
            });
            v.RuleForEach(x => x.Values)
                .Custom((model, context) =>
                {
                    if (model.AttributeValue.IsNullEmptyOrWhiteSpace())
                    {
                        context.AddFailure(nameof(ProductAttributeValueModel.AttributeValue), "Can't add empty values");
                    }
                });
        }

    }
}