using System;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Authentication
{
    public class PasswordChangeModel : FoundationModel, IRequiresValidations<PasswordChangeModel>
    {

        public string CurrentPassword { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }

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