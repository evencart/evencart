#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Extensions;
using FluentValidation;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Genesis.Modules.Meta;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductAttributeModel : GenesisEntityModel, IRequiresValidations<ProductAttributeModel>
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
                var instance = context.InstanceToValidate as ProductAttributeModel;
                if (instance.InputFieldType.RequireValues() && !list.Any())
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