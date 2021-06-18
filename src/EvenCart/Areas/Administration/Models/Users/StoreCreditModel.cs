﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class StoreCreditModel : GenesisEntityModel, IRequiresValidations<StoreCreditModel>
    {
        public int UserId { get; set; }

        public decimal Credit { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime AvailableOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public void SetupValidationRules(ModelValidator<StoreCreditModel> v)
        {
            v.RuleFor(x => x.UserId).GreaterThan(0);
            v.RuleFor(x => x.Description).NotEmpty();
        }
    }
}