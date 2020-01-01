using EvenCart.Core;
using EvenCart.Infrastructure;

namespace Payments.Square
{
    public static class SquareConfig
    {
        public const string PaymentHandlerComponentRouteName = "SquarePaymentHandler";

        public const string SquareReturnUrlRouteName = "SquareReturnUrl";

        public const string SquareCancelUrlRouteName = "SquareCancelUrl";

        public const string SquareIpnHandlerRouteName = "SquareIpnHandler";

        public const string SquareSettingsRouteName = "SquareSettings";

        public const string SquareWebhookUrl = "SquareWebhookUrl";

        public static string UserAgentName = "evencart-" + AppVersionEvaluator.Version;

        public const string ScriptUrl = "https://js.squareup.com/v2/paymentform";

        public const string SandboxScriptUrl = "https://js.squareupsandbox.com/v2/paymentform";

        public const string SandboxConnectUrl = "https://connect.squareupsandbox.com";

    }
}