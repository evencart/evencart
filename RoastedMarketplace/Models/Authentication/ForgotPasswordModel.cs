using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Models.Authentication
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