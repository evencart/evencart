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
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductRelationsModel : FoundationModel, IRequiresValidations<ProductRelationsModel>
    {
        public int ProductId { get; set; }

        public ProductRelationType RelationType { get; set; }

        public IList<int> DestinationProductIds { get; set; }

        public bool IsReciprocal { get; set; }

        public void SetupValidationRules(ModelValidator<ProductRelationsModel> v)
        {
            v.RuleFor(x => x.ProductId).GreaterThan(0);
            v.RuleFor(x => x.DestinationProductIds).Must(x => x.Any());
        }
    }
}