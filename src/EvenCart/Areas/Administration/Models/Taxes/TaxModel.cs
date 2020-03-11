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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Taxes
{
    public class TaxModel : FoundationEntityModel, IRequiresValidations<TaxModel>
    {
        public string Name { get; set; }

        #region Virtual Properties

        public virtual IList<TaxRateModel> TaxRates { get; set; }

        #endregion

        public void SetupValidationRules(ModelValidator<TaxModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}