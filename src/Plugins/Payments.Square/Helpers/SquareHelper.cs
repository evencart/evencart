using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Http;
using Square.Connect.Api;
using Square.Connect.Client;
using Square.Connect.Model;

namespace Payments.Square.Helpers
{
    public class SquareHelper
    {
        private const string SquareCustomerIdKey = "SquareCustomerId";
        private static Configuration _squareConfiguration;
        public static Configuration InitSquare(SquareSettings squareSettings)
        {

            var accessToken = squareSettings.EnableSandbox
                ? squareSettings.SandboxAccessToken
                : squareSettings.AccessToken;
            var config = new Configuration()
            {
                AccessToken = accessToken,
                UserAgent = SquareConfig.UserAgentName,
            };
            if (squareSettings.EnableSandbox)
            {
                config.ApiClient = new ApiClient(SquareConfig.SandboxConnectUrl);
            }

            _squareConfiguration = config;
            return config;
        }

        private static Configuration GetConfiguration(SquareSettings squareSettings)
        {
            _squareConfiguration = _squareConfiguration ?? InitSquare(squareSettings);
            return _squareConfiguration;
        }
        public static TransactionResult ProcessPayment(TransactionRequest request, SquareSettings squareSettings, ILogger logger)
        {
            var order = request.Order;
            var config = GetConfiguration(squareSettings);
            var nonce = request.GetParameterAs<string>("nonce");
            var location = GetApplicationLocations(squareSettings, logger).FirstOrDefault(x => x.Id == squareSettings.LocationId);
            var billingAddress = order.BillingAddressSerialized.To<EvenCart.Data.Entity.Addresses.Address>();
            var shippingAddress = order.ShippingAddressSerialized?.To<EvenCart.Data.Entity.Addresses.Address>();
            var customerId = order.User.GetPropertyValueAs<string>(SquareCustomerIdKey);
            if (customerId.IsNullEmptyOrWhiteSpace())
            {
                var createCustomerRequest = new CreateCustomerRequest(EmailAddress: order.User.Email, GivenName: order.User.Name);
                var customerApi = new CustomersApi(config);
                var customerCreateResponse = customerApi.CreateCustomer(createCustomerRequest);
                customerId = customerCreateResponse.Customer.Id;
            }
            var paymentRequest = new CreatePaymentRequest(SourceId: nonce,
            IdempotencyKey: Guid.NewGuid().ToString(),
            LocationId: location?.Id,
            CustomerId: customerId,
            AmountMoney: new Money()
            {
                Amount = (long)order.OrderTotal * 100,
                Currency = order.CurrencyCode
            },
            AppFeeMoney: new Money()
            {
                Amount = 100 * (long?)order.PaymentMethodFee,
                Currency = order.CurrencyCode
            },
            BillingAddress: new Address(billingAddress.Address1, billingAddress.Address2,
                FirstName: billingAddress.Name, Locality: billingAddress.Landmark,
                PostalCode: billingAddress.ZipPostalCode),
            BuyerEmailAddress: order.User.Email
        );
            if (shippingAddress != null)
                paymentRequest.ShippingAddress = new Address(shippingAddress.Address1, shippingAddress.Address2,
                    FirstName: shippingAddress.Name, Locality: shippingAddress.Landmark,
                    PostalCode: shippingAddress.ZipPostalCode);
            if (squareSettings.AuthorizeOnly)
            {
                paymentRequest.Autocomplete = false; //authorize only
            }
            var paymentsApi = new PaymentsApi(config);
            var paymentResponse = paymentsApi.CreatePayment(paymentRequest);
            var transactionResult = new TransactionResult()
            {
                OrderGuid = order.Guid,
                TransactionGuid = Guid.NewGuid().ToString(),
            };

            if (paymentResponse != null)
            {
                var payment = paymentResponse.Payment;
                transactionResult.Success = true;
                transactionResult.TransactionAmount = (decimal)(payment.AmountMoney.Amount ?? 0) / 100;
                transactionResult.ResponseParameters = new Dictionary<string, object>()
                {
                    { "paymentId", payment.Id },
                    { "referenceId", payment.ReferenceId },
                    { "orderId", payment.OrderId },
                    { "cardDetails", payment.CardDetails }
                };
                if (payment.Status == "APPROVED")
                    transactionResult.NewStatus = PaymentStatus.Authorized;
                else if (payment.Status == "COMPLETED")
                    transactionResult.NewStatus = PaymentStatus.Complete;
                else
                {
                    transactionResult.NewStatus = PaymentStatus.Failed;
                    var errors = string.Join(",", paymentResponse.Errors);
                    logger.Log<TransactionResult>(LogLevel.Warning, "The payment for Order#" + order.Id + " by square failed." + errors);
                    transactionResult.Exception = new Exception("An error occurred while processing payment. Error Details: " + errors);
                    transactionResult.Success = false;
                }

            }
            else
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The payment for Order#" + order.Id + " by square failed. No response received.");
                transactionResult.Success = false;
                transactionResult.Exception = new Exception("An error occurred while processing payment");
            }

            return transactionResult;
        }

        public static TransactionResult ProcessRefund(TransactionRequest refundRequest, SquareSettings squareSettings,
            ILogger logger)
        {
            var order = refundRequest.Order;
            var config = GetConfiguration(squareSettings);
            var paymentId = refundRequest.GetParameterAs<string>("paymentId");
            var refundsApi = new RefundsApi(config);
            var refundResponse = refundsApi.RefundPayment(new RefundPaymentRequest()
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                AmountMoney = new Money()
                {
                    Amount = (refundRequest.IsPartialRefund ? (long?)refundRequest.Amount : (long)order.OrderTotal) * 100
                },
                PaymentId = paymentId,
                Reason = "Refunded by administrator " + ApplicationEngine.CurrentUser.Name
            });

            //perform the call
            var transactionResult = new TransactionResult()
            {
                OrderGuid = order.Guid,
                TransactionGuid = Guid.NewGuid().ToString(),
            };

            if (refundResponse != null)
            {
                var refund = refundResponse.Refund;
                transactionResult.Success = true;
                transactionResult.TransactionAmount = (decimal)(refund.AmountMoney.Amount ?? 0) / 100;
                transactionResult.ResponseParameters = new Dictionary<string, object>()
                {
                    { "refundId", refund.Id },
                };

                if (refund.Status == "PENDING")
                    transactionResult.NewStatus = PaymentStatus.RefundPending;
                else if (refund.Status == "COMPLETE")
                    transactionResult.NewStatus = refundRequest.IsPartialRefund
                        ? PaymentStatus.RefundedPartially
                        : PaymentStatus.Refunded;
                else
                {
                    var errors = string.Join(",", refundResponse.Errors);
                    logger.Log<TransactionResult>(LogLevel.Warning, "The refund for Order#" + order.Id + " by square failed." + errors);
                    transactionResult.Exception = new Exception("An error occurred while refunding payment. Error Details: " + errors);
                    transactionResult.Success = false;
                }

            }
            else
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The refund for Order#" + order.Id + " by square failed. No response received.");
                transactionResult.Success = false;
                transactionResult.Exception = new Exception("An error occurred while refunding payment");
            }

            if (transactionResult.NewStatus == PaymentStatus.RefundPending)
            {
                //request again to get exact status
                var refund = refundsApi.GetPaymentRefund(transactionResult.GetParameterAs<string>("refundId"));
                if (refund.Refund.Status == "COMPLETE")
                {
                    transactionResult.NewStatus = refundRequest.IsPartialRefund ? PaymentStatus.RefundedPartially : PaymentStatus.Refunded;
                }
            }
            return transactionResult;
        }
        public static TransactionResult ProcessVoid(TransactionRequest voidRequest, SquareSettings squareSettings, ILogger logger)
        {
            var order = voidRequest.Order;
            var config = GetConfiguration(squareSettings);
            var paymentId = voidRequest.GetParameterAs<string>("paymentId");
            var paymentsApi = new PaymentsApi(config);
            //perform the call
            var paymentResponse = paymentsApi.CancelPayment(paymentId);
            var transactionResult = new TransactionResult()
            {
                OrderGuid = order.Guid,
                TransactionGuid = Guid.NewGuid().ToString(),
            };

            if (paymentResponse != null)
            {
                var payment = paymentResponse.Payment;
                transactionResult.Success = true;
                transactionResult.TransactionAmount = (decimal)(payment.AmountMoney.Amount ?? 0) / 100;
                transactionResult.ResponseParameters = new Dictionary<string, object>()
                {
                    { "PaymentId", payment.Id },
                    { "ReferenceId", payment.ReferenceId }
                };

                if (payment.Status == "COMPLETED")
                    transactionResult.NewStatus = PaymentStatus.Complete;
                else
                {
                    transactionResult.NewStatus = PaymentStatus.Failed;
                    var errors = string.Join(",", paymentResponse.Errors);
                    logger.Log<TransactionResult>(LogLevel.Warning, "The void for Order#" + order.Id + " by square failed." + errors);
                    transactionResult.Exception = new Exception("An error occurred while voiding payment. Error Details: " + errors);
                    transactionResult.Success = false;
                }

            }
            else
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The void for Order#" + order.Id + " by square failed. No response received.");
                transactionResult.Success = false;
                transactionResult.Exception = new Exception("An error occurred while voiding payment");
            }

            return transactionResult;
        }

        public static TransactionResult ProcessCapture(TransactionRequest captureRequest, SquareSettings squareSettings, ILogger logger)
        {
            var order = captureRequest.Order;
            var config = GetConfiguration(squareSettings);
            var paymentId = captureRequest.GetParameterAs<string>("paymentId");
            var paymentsApi = new PaymentsApi(config);
            //perform the call
            var paymentResponse = paymentsApi.CompletePayment(paymentId);
            var transactionResult = new TransactionResult()
            {
                OrderGuid = order.Guid,
                TransactionGuid = Guid.NewGuid().ToString(),
            };

            if (paymentResponse != null)
            {
                var payment = paymentResponse.Payment;
                transactionResult.Success = true;
                transactionResult.TransactionAmount = (decimal)(payment.AmountMoney.Amount ?? 0) / 100;
                transactionResult.ResponseParameters = new Dictionary<string, object>()
                {
                    { "PaymentId", payment.Id },
                    { "ReferenceId", payment.ReferenceId }
                };

                if (payment.Status == "COMPLETED")
                    transactionResult.NewStatus = PaymentStatus.Complete;
                else
                {
                    transactionResult.NewStatus = PaymentStatus.Failed;
                    var errors = string.Join(",", paymentResponse.Errors);
                    logger.Log<TransactionResult>(LogLevel.Warning, "The capture for Order#" + order.Id + " by square failed." + errors);
                    transactionResult.Exception = new Exception("An error occurred while capturing payment. Error Details: " + errors);
                    transactionResult.Success = false;
                }

            }
            else
            {
                logger.Log<TransactionResult>(LogLevel.Warning, "The capture for Order#" + order.Id + " by square failed. No response received.");
                transactionResult.Success = false;
                transactionResult.Exception = new Exception("An error occurred while capturing payment");
            }

            return transactionResult;
        }


        public static TransactionResult CreateSubscription(TransactionRequest request, SquareSettings squareSettings,
            ILogger logger)
        {
            return null;
        }

        public static TransactionResult StopSubscription(TransactionRequest request, SquareSettings squareSettings, ILogger logger)
        {
            return null;
        }
        public static async void ParseWebhookResponse(HttpRequest responseRequest)
        {

        }


        private static IList<Location> GetApplicationLocations(SquareSettings squareSettings, ILogger logger)
        {
            try
            {
                var config = GetConfiguration(squareSettings);
                var locationsApi = new LocationsApi(config);
                var locationsResponse = locationsApi.ListLocations();
                if (locationsResponse?.Locations == null)
                {
                    throw new Exception("Empty locations response received");
                }
                if (locationsResponse.Errors?.Any() ?? false)
                {
                    throw new Exception("Errors occurred while retrieving locations." +
                                        string.Join(",", locationsResponse.Errors));
                }
                //filter active locations that can process cards
                var activeLocations = locationsResponse.Locations
                    .Where(x => x.Status == "ACTIVE" && (x.Capabilities == null || x.Capabilities.Contains("CREDIT_CARD_PROCESSING"))).ToList();
                return activeLocations;
            }
            catch (Exception ex)
            {
                logger.Log<SquareHelper>(LogLevel.Error, ex.Message, ex);
                return new List<Location>();
            }
        }
    }
}