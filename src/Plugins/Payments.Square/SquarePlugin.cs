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
using EvenCart.Services.Logger;
using Payments.Square.Helpers;

namespace Payments.Square
{
    public class SquarePlugin : FoundationPlugin, IPaymentHandlerPlugin
    {
        private readonly SquareSettings _squareSettings;
        private readonly ILogger _logger;
        public SquarePlugin(SquareSettings squareSettings, ILogger logger)
        {
            _squareSettings = squareSettings;
            _logger = logger;
        }

        public PaymentMethodType PaymentMethodType => PaymentMethodType.CreditCard;

        public string PaymentHandlerComponentRouteName => SquareConfig.PaymentHandlerComponentRouteName;

        public PaymentOperation[] SupportedOperations => new[]
            {PaymentOperation.Authorize, PaymentOperation.Capture, PaymentOperation.Refund, PaymentOperation.Void};

        public bool SupportsSubscriptions => false;

        public TransactionResult ProcessTransaction(TransactionRequest request)
        {
            if (request.RequestType == TransactionRequestType.Payment)
                return SquareHelper.ProcessPayment(request, _squareSettings, _logger);
            if (request.RequestType == TransactionRequestType.Refund)
                return SquareHelper.ProcessRefund(request, _squareSettings, _logger);
            if (request.RequestType == TransactionRequestType.Void)
                return SquareHelper.ProcessVoid(request, _squareSettings, _logger);
            if (request.RequestType == TransactionRequestType.Capture)
                return SquareHelper.ProcessCapture(request, _squareSettings, _logger);
            if(request.RequestType == TransactionRequestType.SubscriptionCreate)
                return SquareHelper.CreateSubscription(request, _squareSettings, _logger);
            if (request.RequestType == TransactionRequestType.SubscriptionCancel)
                return SquareHelper.StopSubscription(request, _squareSettings, _logger);
            return null;
        }

        public decimal GetPaymentHandlerFee(Cart cart)
        {
            return _squareSettings.UsePercentageForAdditionalFee
                ? _squareSettings.AdditionalFee * cart.FinalAmount / 100
                : _squareSettings.AdditionalFee;
        }

        public decimal GetPaymentHandlerFee(Order order)
        {
            return _squareSettings.UsePercentageForAdditionalFee
                ? _squareSettings.AdditionalFee * order.OrderTotal / 100
                : _squareSettings.AdditionalFee;
        }

        public bool ValidatePaymentInfo(Dictionary<string, string> parameters, out string error)
        {
            error = null;
            parameters.TryGetValue("nonce", out var nonce);
            if (nonce.IsNullEmptyOrWhiteSpace())
            {
                error = LocalizationHelper.Localize("The card nonce was not correct.");
                return false;
            }
            return true;
        }

        public override string ConfigurationUrl =>
            ApplicationEngine.RouteUrl(SquareConfig.SquareSettingsRouteName);

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
