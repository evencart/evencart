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

namespace EvenCart.Areas.Administration.Models.Taxes
{
    public class TaxRateModel : FoundationEntityModel, IRequiresValidations<TaxRateModel>
    {
        public int TaxId { get; set; }

        public int CountryId { get; set; }

        public int? StateOrProvinceId { get; set; }

        public string ZipOrPostalCode { get; set; }

        public string CountryName { get; set; }

        public string StateOrProvinceName { get; set; }

        public decimal Rate { get; set; } = 0;

        public void SetupValidationRules(ModelValidator<TaxRateModel> v)
        {
            v.RuleFor(x => x.TaxId).GreaterThan(0);
        }
    }
}