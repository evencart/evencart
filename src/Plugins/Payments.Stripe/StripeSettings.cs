using EvenCart.Core.Config;

namespace Payments.Stripe
{
    public class StripeSettings : ISettingGroup
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
    }
}