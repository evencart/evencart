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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductVariantModel : FoundationEntityModel, IRequiresValidations<ProductVariantModel>
    {
        public IList<ProductAttributeModel> Attributes { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public decimal? Price { get; set; }

        public bool TrackInventory { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public bool DisableSale { get; set; }

        public void SetupValidationRules(ModelValidator<ProductVariantModel> v)
        {
            v.RuleFor(x => x.Attributes)
                .Must(x => x.Any(y => y.Id > 0))
                .WithMessage("At least one attribute combination must be passed to setup a variant.")
                .Must(x => x.Any(y => y.Values.Any(z => z.Id > 0)))
                .WithMessage("At least one attribute combination must be passed to setup a variant.");
        }
    }
}