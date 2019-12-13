using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Payments;
using EvenCart.Services.Plugins;
using EvenCart.Infrastructure;
using Payments.PaypalWithRedirect.Helpers;

namespace Payments.PaypalWithRedirect
{
    public class PaypalWithRedirectPlugin : FoundationPlugin, IPaymentHandlerPlugin
    {
        private readonly PaypalWithRedirectSettings _paypalWithRedirectSettings;
        public PaypalWithRedirectPlugin(PaypalWithRedirectSettings paypalWithRedirectSettings)
        {
            _paypalWithRedirectSettings = paypalWithRedirectSettings;
        }

        public PaymentMethodType PaymentMethodType => PaymentMethodType.EWallet;

        public string PaymentHandlerComponentRouteName => PaypalConfig.PaymentHandlerComponentRouteName;

        public PaymentOperation[] SupportedOperations => new[]
            {PaymentOperation.Authorize, PaymentOperation.Capture, PaymentOperation.Refund, PaymentOperation.Void};

        public bool SupportsSubscriptions { get; } = false;

        public TransactionResult ProcessTransaction(TransactionRequest request)
        {
            var order = request.Order;
            var transactionResult = PaypalHelper.ProcessApproval(order, _paypalWithRedirectSettings);
            return transactionResult;
        }

        public decimal GetPaymentHandlerFee(Cart cart)
        {
            return 0;
        }

        public decimal GetPaymentHandlerFee(Order order)
        {
            return 0;
        }

        public bool ValidatePaymentInfo(Dictionary<string, string> parameters, out string error)
        {
            //validation will be done at paypal
            error = null;
            return true;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(PaypalConfig.PaypalWithRedirectSettingsRouteName);

        #region Helpers
        private static bool Mod10Check(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            var sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                .Reverse()
                .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                .Sum((e) => e / 10 + e % 10);

            return sumOfDigits % 10 == 0;
        }

        private static bool ExpiryCheck(string month, string year)
        {
            if (!int.TryParse(month, out int monthInt))
                return false;
            if (!int.TryParse(year, out int yearInt))
                return false;
            var now = DateTime.UtcNow;
            return monthInt >= now.Month && yearInt >= now.Year;
        }

        private static string GetRawCreditNumber(string creditCardNumber)
        {
            return creditCardNumber.Replace("-", "");
        }
        #endregion
    }
}
