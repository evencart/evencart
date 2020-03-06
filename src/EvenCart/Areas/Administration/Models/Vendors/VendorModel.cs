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

using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Vendors
{
    public class VendorModel : FoundationEntityModel, IRequiresValidations<VendorModel>
    {
        public string Name { get; set; }

        public string GstNumber { get; set; }

        public string Tin { get; set; }

        public string Pan { get; set; }

        public string Address { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public int CountryId { get; set; }

        public string ZipPostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public VendorStatus VendorStatus { get; set; }

        public bool SendNotification { get; set; }

        public void SetupValidationRules(ModelValidator<VendorModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Address).NotEmpty();
        }
    }
}