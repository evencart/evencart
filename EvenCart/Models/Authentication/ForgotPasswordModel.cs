using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Authentication
{
    public class ForgotPasswordModel : FoundationModel, IRequiresValidations<ForgotPasswordModel>
    {
        public string Email { get; set; }

        public void SetupValidationRules(ModelValidator<ForgotPasswordModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}