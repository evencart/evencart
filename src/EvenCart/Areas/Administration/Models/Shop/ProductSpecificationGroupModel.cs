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

using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductSpecificationGroupModel : GenesisEntityModel, IRequiresValidations<ProductSpecificationGroupModel>
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public int ProductId { get; set; }

        public void SetupValidationRules(ModelValidator<ProductSpecificationGroupModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }

        public override bool Equals(object obj)
        {
            if (obj is ProductSpecificationGroupModel grpModel)
            {
                return this.Name == grpModel.Name && this.ProductId == grpModel.ProductId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // Maybe nullity checks, if these are objects not primitives!
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + ProductId.GetHashCode();
                return hash;
            }
        }
    }
}