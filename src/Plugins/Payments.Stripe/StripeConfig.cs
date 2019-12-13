namespace Payments.Stripe
{
    public static class StripeConfig
    {
        public const string PaymentHandlerComponentRouteName = "StripePaymentHandler";

        public const string StripeReturnUrlRouteName = "StripeReturnUrl";

        public const string StripeCancelUrlRouteName = "StripeCancelUrl";

        public const string StripeIpnHandlerRouteName = "StripeIpnHandler";

        public const string StripeSettingsRouteName = "StripeSettings";

        public const string StripeWebhookUrl = "StripeWebhookUrl";
    }
}