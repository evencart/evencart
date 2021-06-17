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
using FluentValidation;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductSpecificationModel : GenesisEntityModel, IRequiresValidations<ProductSpecificationModel>
    {
        public string Name { get; set; }

        public string Label { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsVisible { get; set; }

        public bool IsFilterable { get; set; }

        public int ProductSpecificationGroupId { get; set; }

        public ProductSpecificationGroupModel ProductSpecificationGroup { get; set; }

        public List<ProductSpecificationValueModel> Values { get; set; } = new List<ProductSpecificationValueModel>();

        public List<SelectListItem> ValuesAsSelectListItems => SelectListHelper.GetSelectItemList(Values, x => x.Id,
            x => x.AttributeValue);

        public void SetupValidationRules(ModelValidator<ProductSpecificationModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty().When(x => x.Id == 0);
            v.RuleFor(x => x.Values).Custom((list, context) =>
            {
                if (!list.Any())
                {
                    context.AddFailure(nameof(ProductSpecificationValueModel.AttributeValue), "At least one specification value must be provided");
                }
            });
            v.RuleForEach(x => x.Values)
                .Custom((model, context) =>
                {
                    if (model.AttributeValue.IsNullEmptyOrWhiteSpace())
                    {
                        context.AddFailure(nameof(ProductSpecificationValueModel.AttributeValue), "Can't add empty values");
                    }
                });
        }

    }
}