using System;
using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Extensions;
using EvenCart.Services.Payments;
using EvenCart.Services.Plugins;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Services.Helpers;
using EvenCart.Services.Logger;
using Payments.Stripe.Helpers;

namespace Payments.Stripe
{
    public class StripePlugin : FoundationPlugin, IPaymentHandlerPlugin
    {
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger _logger;
        public StripePlugin(StripeSettings stripeSettings, ILogger logger)
        {
            _stripeSettings = stripeSettings;
            _logger = logger;
        }

        public PaymentMethodType PaymentMethodType => PaymentMethodType.CreditCard;

        public string PaymentHandlerComponentRouteName => StripeConfig.PaymentHandlerComponentRouteName;

        public PaymentOperation[] SupportedOperations => new[]
            {PaymentOperation.Authorize, PaymentOperation.Capture, PaymentOperation.Refund, PaymentOperation.Void};

        public bool SupportsSubscriptions => true;

        public TransactionResult ProcessTransaction(TransactionRequest request)
        {
            if (request.RequestType == TransactionRequestType.Payment)
                return StripeHelper.ProcessPayment(request, _stripeSettings, _logger);
            if (request.RequestType == TransactionRequestType.Refund)
                return StripeHelper.ProcessRefund(request, _stripeSettings, _logger);
            if (request.RequestType == TransactionRequestType.Void)
                return StripeHelper.ProcessVoid(request, _stripeSettings, _logger);
            if (request.RequestType == TransactionRequestType.Capture)
                return StripeHelper.ProcessCapture(request, _stripeSettings, _logger);
            if(request.RequestType == TransactionRequestType.SubscriptionCreate)
                return StripeHelper.CreateSubscription(request, _stripeSettings, _logger);
            if (request.RequestType == TransactionRequestType.SubscriptionCancel)
                return StripeHelper.StopSubscription(request, _stripeSettings, _logger);
            return null;
        }

        public decimal GetPaymentHandlerFee(Cart cart)
        {
            return _stripeSettings.UsePercentageForAdditionalFee
                ? _stripeSettings.AdditionalFee * cart.FinalAmount / 100
                : _stripeSettings.AdditionalFee;
        }

        public decimal GetPaymentHandlerFee(Order order)
        {
            return _stripeSettings.UsePercentageForAdditionalFee
                ? _stripeSettings.AdditionalFee * order.OrderTotal / 100
                : _stripeSettings.AdditionalFee;
        }

        public bool ValidatePaymentInfo(Dictionary<string, string> parameters, out string error)
        {
            error = null;
            parameters.TryGetValue("cardNumber", out var cardNumber);
            parameters.TryGetValue("cardName", out var cardName);
            parameters.TryGetValue("expireMonth", out var expireMonthStr);
            parameters.TryGetValue("expireYear", out var expireYearStr);
            parameters.TryGetValue("cvv", out var cvv);

            if (!PaymentCardHelper.IsCardNumberValid(cardNumber))
            {
                error = LocalizationHelper.Localize("The card number is incorrect");
                return false;
            }
            if (cardName.IsNullEmptyOrWhiteSpace())
            {
                error = LocalizationHelper.Localize("The card name can not be empty");
                return false;
            }
            if (cvv.IsNullEmptyOrWhiteSpace())
            {
                error = LocalizationHelper.Localize("The card security code can not be empty");
                return false;
            }

            try
            {
                if (ExpiryCheck(expireMonthStr, expireYearStr))
                {
                    error = LocalizationHelper.Localize("The card expiry is incorrect or card has expired");
                    return false;
                }
            }
            catch
            {
                error = LocalizationHelper.Localize("The card expiry is incorrect or card has expired");
                return false;
            }
          
            return true;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(StripeConfig.StripeSettingsRouteName);

        #region Helpers
        private static bool ExpiryCheck(string month, string year)
        {
            if (!int.TryParse(month, out var monthInt))
                return false;
            if (!int.TryParse(year, out var yearInt))
                return false;
            var now = DateTime.UtcNow;
            return monthInt >= now.Month && yearInt >= now.Year;
        }
        #endregion
    }
}
