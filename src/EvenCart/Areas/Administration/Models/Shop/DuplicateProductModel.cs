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

using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    /// <summary>
    /// Represents a duplicate product model
    /// </summary>
    public class DuplicateProductModel : FoundationModel, IRequiresValidations<DuplicateProductModel>
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public bool DuplicateProductAttributes { get; set; }

        public bool DuplicateSpecificationAttributes { get; set; }

        public bool DuplicateCategories { get; set; }

        public bool DuplicateMedia { get; set; }

        public bool DuplicateVariants { get; set; }

        public bool DuplicateInventory { get; set; }

        public bool DuplicateDownloads { get; set; }

        public bool DuplicateVendors { get; set; }

        public void SetupValidationRules(ModelValidator<DuplicateProductModel> v)
        {
            v.RuleFor(x => x.ProductId).GreaterThan(0);

            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}