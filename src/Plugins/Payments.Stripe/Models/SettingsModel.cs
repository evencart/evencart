using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace Payments.Stripe.Models
{
    public class SettingsModel : FoundationModel, IRequiresValidations<SettingsModel>
    {
        public string PublishableKey { get; set; }

        public string SecretKey { get; set; }

        public bool EnableTestMode { get; set; }

        public string TestPublishableKey { get; set; }

        public string TestSecretKey { get; set; }

        public bool AuthorizeOnly { get; set; }

        public bool UsePercentageForAdditionalFee { get; set; }

        public decimal AdditionalFee { get; set; }

        public string Description { get; set; }

        public void SetupValidationRules(ModelValidator<SettingsModel> v)
        {
            v.RuleFor(x => x.PublishableKey).NotEmpty();
            v.RuleFor(x => x.SecretKey).NotEmpty();
            v.RuleFor(x => TestPublishableKey).NotEmpty().When(x => x.EnableTestMode);
            v.RuleFor(x => TestSecretKey).NotEmpty().When(x => x.EnableTestMode);
        }
    }
}