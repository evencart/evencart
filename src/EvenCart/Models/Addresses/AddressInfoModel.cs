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
using Genesis.Modules.Addresses;

namespace EvenCart.Models.Addresses
{
    public class AddressInfoModel : GenesisEntityModel, IRequiresValidations<AddressInfoModel>
    {
        /// <summary>
        /// The name of the addressee
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// House number, apartment/building name
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Street name, locality
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// A nearby landmark for easy location
        /// </summary>
        public string Landmark { get; set; }

        /// <summary>
        /// The state or province id
        /// </summary>
        public int? StateProvinceId { get; set; }

        /// <summary>
        /// The state or province name if state or province id is not known
        /// </summary>
        public string StateProvinceName { get; set; }

        /// <summary>
        /// The name of city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The postal code 
        /// </summary>
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// The country id. See <a href="#countries">Country</a> endpoints to get country info.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The name of the country
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// The phone number of the addressee
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The website address of the addressee
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// The emai laddress of the addressee
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The address type
        /// </summary>
        public AddressType AddressType { get; set; } = AddressType.Home;

        public void SetupValidationRules(ModelValidator<AddressInfoModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Address1).NotEmpty();
            v.RuleFor(x => x.City).NotEmpty();
            v.RuleFor(x => x.ZipPostalCode).NotEmpty();
            v.RuleFor(x => x.CountryId).NotEmpty().GreaterThan(0);
            v.RuleFor(x => x.StateProvinceName).NotEmpty()
                .When(x => !x.StateProvinceId.HasValue || x.StateProvinceId <= 0);
        }
    }
}