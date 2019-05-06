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
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IList<ConsentModel> Consents { get; set; }

        public void SetupValidationRules(ModelValidator<RegisterModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty();
            v.RuleFor(x => x.Password).NotEmpty();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}