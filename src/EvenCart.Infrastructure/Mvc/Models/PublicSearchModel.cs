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

using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Infrastructure.Mvc.Models
{
    public class PublicSearchModel : FoundationModel, IRequiresValidations<PublicSearchModel>
    {
        public string Search { get; set; }

        public int Page { get; set; } = 1;

        public int Count { get; set; } = 15;

        public void SetupValidationRules(ModelValidator<PublicSearchModel> v)
        {
            v.RuleFor(x => x.Page).GreaterThan(0);
            v.RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}