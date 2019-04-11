using EvenCart.Core.Config;

namespace Payments.PaypalWithRedirect
{
    public class PaypalWithRedirectSettings : ISettingGroup
    {
        public bool EnableSandbox { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string CheckoutProfileId { get; set; }
    }
}