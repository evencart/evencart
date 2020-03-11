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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Authentication
{
    public class PasswordChangeModel : FoundationModel, IRequiresValidations<PasswordChangeModel>
    {
        /// <summary>
        /// The current password of the user. Ignore if a valid code parameter is being passed
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The code for changing the password. Ignore if currentPassword parameter is being passed
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The new password for the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The confirm password for the user. Should be same as password field
        /// </summary>
        public string ConfirmPassword { get; set; }

        public void SetupValidationRules(ModelValidator<PasswordChangeModel> v)
        {
            v.RuleFor(x => x.Code).NotEmpty();
            v.RuleFor(x => x.Password).NotEmpty();
            v.RuleFor(x => x.ConfirmPassword).NotEmpty();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}