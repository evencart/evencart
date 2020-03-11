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

namespace EvenCart.Models.Authentication
{
    public class LoginModel : FoundationModel, IRequiresValidations<LoginModel>
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// If a long live cookie should be created. The parameter is ignored if token is set to true
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// The url where the user be redirected after login. The parameter is sent with response.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// If true sends an authentication token in the response. If false, sends an authentication cookie.
        /// </summary>
        public bool Token { get; set; }

        public void SetupValidationRules(ModelValidator<LoginModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.Password).NotEmpty();
        }
    }
}