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

using EvenCart.Areas.Administration.Models.Addresses;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    public class WarehouseModel : GenesisEntityModel, IRequiresValidations<WarehouseModel>
    {
        /// <summary>
        /// The address details of the warehouse
        /// </summary>
        public AddressModel Address { get; set; }

        /// <summary>
        /// The display order of the warehouse
        /// </summary>
        public int DisplayOrder { get; set; }

        public void SetupValidationRules(ModelValidator<WarehouseModel> v)
        {
            v.RuleFor(x => x.Address).NotNull();
            v.RuleFor(x => x.Address.Name).NotEmpty();
            v.RuleFor(x => x.Address.Address1).NotEmpty();
            v.RuleFor(x => x.Address.City).NotEmpty();
            v.RuleFor(x => x.Address.CountryId).NotEmpty();
            v.RuleFor(x => x.Address.ZipPostalCode).NotEmpty();
        }
    }
}