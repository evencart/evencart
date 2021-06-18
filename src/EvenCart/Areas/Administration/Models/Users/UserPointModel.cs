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

using System;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class UserPointModel : GenesisEntityModel, IRequiresValidations<UserPointModel>
    {
        public int UserId { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Reason { get; set; }

        public int ActivatorUserId { get; set; }

        public UserMiniModel ActivatorUser { get; set; }

        public void SetupValidationRules(ModelValidator<UserPointModel> v)
        {
            v.RuleFor(x => x.UserId).GreaterThan(0);
            v.RuleFor(x => x.Points).NotEqual(0).When(x => x.Id < 1);
        }
    }
}