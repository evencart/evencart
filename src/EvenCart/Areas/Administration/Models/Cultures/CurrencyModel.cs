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
using Genesis.Modules.Localization;

namespace EvenCart.Areas.Administration.Models.Cultures
{
    public class CurrencyModel : GenesisEntityModel, IRequiresValidations<CurrencyModel>
    {
        public string Name { get; set; }

        public string IsoCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public string CultureCode { get; set; }

        public string CustomFormat { get; set; }

        public string Flag { get; set; }

        public bool Published { get; set; }

        public Rounding RoundingType { get; set; }

        public int NumberOfDecimalPlaces { get; set; }

        public string FlagUrl { get; set; }

        public void SetupValidationRules(ModelValidator<CurrencyModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.IsoCode).NotEmpty();
        }
    }
}