using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace Payments.PaypalWithRedirect.Models
{
    public class SettingsModel : FoundationModel, IRequiresValidations<SettingsModel>
    {
        public bool EnableSandbox { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public void SetupValidationRules(ModelValidator<SettingsModel> v)
        {
            v.RuleFor(x => x.ClientSecret).NotEmpty();
            v.RuleFor(x => x.ClientId).NotEmpty();
        }
    }
}