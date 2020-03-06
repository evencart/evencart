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
    /// <summary>
    /// Represents an email verification object
    /// </summary>
    public class EmailVerificationModel : FoundationModel, IRequiresValidations<EmailVerificationModel>
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The code for verification
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Set to true if a token should be returned upon successful login
        /// </summary>
        public bool Token { get; set; }

        public void SetupValidationRules(ModelValidator<EmailVerificationModel> v)
        {
            v.RuleFor(x => x.Code).NotEmpty();
            v.RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}