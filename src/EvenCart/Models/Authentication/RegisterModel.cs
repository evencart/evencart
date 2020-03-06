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
using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using EvenCart.Models.Gdpr;
using FluentValidation;

namespace EvenCart.Models.Authentication
{
    public class RegisterModel : FoundationModel, IRequiresValidations<RegisterModel>
    {
        /// <summary>
        /// The email of new user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password that'll be used for login. It is case sensitive.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The password that'll be used for login. This should be same as <see cref="Password"></see> and is case sensitive.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// The <see cref="ConsentModel">consents</see> that the user has agreed or denied to. 
        /// </summary>
        public IList<ConsentModel> Consents { get; set; }

        /// <summary>
        /// The invite code if any for registration
        /// </summary>
        public string InviteCode { get; set; }

        public void SetupValidationRules(ModelValidator<RegisterModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty();
            v.RuleFor(x => x.Password).NotEmpty();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}