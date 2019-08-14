using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Enum;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using Stripe;

namespace Payments.Stripe.Helpers
{
    public class StripeHelper
    {
        private static void InitStripe(StripeSettings stripeSettings, bool secret = false)
        {
            var publishableKey = stripeSettings.EnableTestMode
                ? stripeSettings.TestPublishableKey
                : stripeSettings.PublishableKey;

            var secretKey = stripeSettings.EnableTestMode
                ? stripeSettings.TestSecretKey
                : stripeSettings.SecretKey;
            StripeConfiguration.ApiKey = !secret ? publishableKey : secretKey;
        }
        public static TransactionResult ProcessPayment(TransactionRequest request, StripeSettings stripeSettings, ILogger logger)
        {
            InitStripe(stripeSettings);
            var parameters = request.Parameters;
            parameters.TryGetValue("cardNumber", out var cardNumber);
            parameters.TryGetValue("cardName", out var cardName);
            parameters.TryGetValue("expireMonth", out var expireMonthStr);
            parameters.TryGetValue("expireYear", out var expireYearStr);
            parameters.TryGetValue("cvv", out var cvv);

            var tokenCreateOptions = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = cardNumber.ToString(),
                    ExpYear = long.Parse(expireYearStr.ToString()),
                    ExpMonth = long.Parse(expireMonthStr.ToString()),
                    Cvc = cvv.ToString(),
                    Name = cardName.ToString(),
                }
            };
            //get token for card
            var tokenService = new TokenService();
            var stripeToken = tokenService.Create(tokenCreateOptions);
            var order = request.Order;
            var options = new ChargeCreateOptions
            {
                Amount = (long)(order.OrderTotal) * 100,
                Currency = order.CurrencyCode.ToLower(),
                Description = stripeSettings.Description,
                Source = stripeToken.Id,
                Capture = !stripeSettings.AuthorizeOnly
            };
            InitStripe(stripeSettings, true);
            var service = new ChargeService();
            var charge = service.Create(options);
            var processPaymentResult = new TransactionResult()
            {
                ResponseParameters = new Dictionary<string, object>()
                {
                    {"authorizationCode", charge.AuthorizationCode },
                    {"balanceTransactionId", charge.BalanceTransactionId },
                    {"chargeId", charge.Id }
                },
                TransactionGuid = request.TransactionGuid,
                TransactionAmount = (decimal)charge.Amount / 100,
                TransactionCurrencyCode = charge.Currency,
                OrderGuid = order.Guid
            };
            if (charge.Status == "failed")
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The payment for Order#" + order.Id + " by stripe failed." + charge.FailureMessage);
                processPaymentResult.Success = false;
                processPaymentResult.Exception = new Exception("An error occurred while processing payment");
                return processPaymentResult;
            }

            processPaymentResult.Success = true;
            if (charge.Status == "succeeded")
                processPaymentResult.NewStatus =
                    stripeSettings.AuthorizeOnly ? PaymentStatus.Authorized : PaymentStatus.Complete;
            else
            {
                processPaymentResult.NewStatus = PaymentStatus.Pending;
            }
            return processPaymentResult;
        }

        public static TransactionResult ProcessRefund(TransactionRequest refundRequest, StripeSettings stripeSettings, ILogger logger)
        {
            InitStripe(stripeSettings, true);
            var refundService = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                ChargeId = refundRequest.GetParameterAs<string>("chargeId"),
                Amount = 100 * (long)(refundRequest.Amount ?? refundRequest.Order.OrderTotal),
            };
            var refund = refundService.Create(refundOptions);
            var refundResult = new TransactionResult()
            {
                TransactionGuid = Guid.NewGuid().ToString(),
                ResponseParameters = new Dictionary<string, object>()
                {
                    {"sourceTransferReversalId", refund.SourceTransferReversalId },
                    {"balanceTransactionId", refund.BalanceTransactionId },
                    {"chargeId", refund.ChargeId },
                    {"receiptNumber", refund.ReceiptNumber },
                },
                OrderGuid = refundRequest.Order.Guid,
                TransactionCurrencyCode = refund.Currency,
                TransactionAmount = (decimal)refund.Amount / 100
            };
            if (refund.Status == "failed")
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The refund for Order#" + refundRequest.Order.Id + " by stripe failed." + refund.FailureReason);
                refundResult.Success = false;
                refundResult.Exception = new Exception("An error occurred while processing refund");
                return refundResult;
            }
            if (refund.Status == "succeeded")
            {
                refundResult.NewStatus = refundRequest.IsPartialRefund ? PaymentStatus.RefundedPartially : PaymentStatus.Refunded;
                refundResult.Success = true;
            }

            return refundResult;
        }

        public static TransactionResult ProcessCapture(TransactionRequest captureRequest, StripeSettings stripeSettings, ILogger logger)
        {
            InitStripe(stripeSettings, true);
            var chargeService = new ChargeService();
            var chargeId = captureRequest.GetParameterAs<string>("chargeId");
            var charge = chargeService.Capture(chargeId, new ChargeCaptureOptions()
            {
                Amount = 100 * (long)(captureRequest.Amount ?? captureRequest.Order.OrderTotal)
            });
            var captureResult = new TransactionResult()
            {
                ResponseParameters = new Dictionary<string, object>()
                {
                    {"chargeId", charge.Id },
                    {"balanceTransactionId", charge.BalanceTransactionId },
                    {"disputeId", charge.DisputeId },
                    {"invoiceId", charge.InvoiceId },
                },
                OrderGuid = captureRequest.Order.Guid,
                TransactionAmount = (decimal)charge.Amount / 100
            };
            if (charge.Status == "failed")
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The capture for Order#" + captureRequest.Order.Id + " by stripe failed.");
                captureResult.Success = false;
                captureResult.Exception = new Exception("An error occurred while processing capture");
                return captureResult;
            }
            if (charge.Captured.GetValueOrDefault())
            {
                captureResult.NewStatus = PaymentStatus.Complete;
                captureResult.Success = true;
            }

            return captureResult;
        }
    }
}