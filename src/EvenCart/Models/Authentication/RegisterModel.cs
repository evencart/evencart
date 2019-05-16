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

        public void SetupValidationRules(ModelValidator<RegisterModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty();
            v.RuleFor(x => x.Password).NotEmpty();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}