using System;
using System.Collections.Generic;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Extensions;
using EvenCart.Services.Settings;
using EvenCart.Infrastructure;
using Payments.PaypalWithRedirect.Models;
using PayPal.Core;
using PayPal.v1.PaymentExperience;
using PayPal.v1.Payments;
using Order = EvenCart.Data.Entity.Purchases.Order;
namespace Payments.PaypalWithRedirect.Helpers
{
    public class PaypalHelper
    {
        private static PayPalEnvironment GetEnvironment(PaypalWithRedirectSettings settings)
        {
            if (settings.EnableSandbox)
                return new SandboxEnvironment(settings.ClientId, settings.ClientSecret);

            return new LiveEnvironment(settings.ClientId, settings.ClientSecret);
        }

        public static TransactionResult ProcessApproval(Order order, PaypalWithRedirectSettings settings)
        {
            var payer = new Payer()
            {
                PaymentMethod = "paypal",
            };

            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = order.OrderTotal.ToString("N"),
                            Currency = order.CurrencyCode
                        }
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    ReturnUrl = ApplicationEngine.RouteUrl(PaypalConfig.PaypalWithRedirectReturnUrlRouteName, new {orderGuid = order.Guid}, absoluteUrl: true),
                    CancelUrl = ApplicationEngine.RouteUrl(PaypalConfig.PaypalWithRedirectCancelUrlRouteName, new { orderGuid = order.Guid }, absoluteUrl: true),
                },
                Payer = payer
            };
            var pcRequest = new PaymentCreateRequest();
            pcRequest.RequestBody(payment);

            var environment = GetEnvironment(settings);

            var client = new PayPalHttpClient(environment);
            var transactionResult = new TransactionResult();
            try
            {
                var response = client.Execute(pcRequest).Result;
                var result = response.Result<Payment>();

                string redirectUrl = null;
                foreach (var link in result.Links)
                {
                    if (link.Rel.Equals("approval_url"))
                    {
                        redirectUrl = link.Href;
                    }
                }

                if (redirectUrl == null)
                {
                    transactionResult.Success = false;
                    transactionResult.Exception = new Exception("Failed to get approval url");
                }
                else
                {
                    transactionResult.Success = true;
                    transactionResult.NewStatus = PaymentStatus.Authorized;
                    transactionResult.Redirect(redirectUrl);
                }
            }
            catch (BraintreeHttp.HttpException ex)
            {
                transactionResult.Exception = ex;
            }

            return transactionResult;
        }

        public static TransactionResult ProcessExecution(Order order, PaymentReturnModel returnModel, PaypalWithRedirectSettings settings)
        {
            var payment = new PaymentExecution()
            {
                PayerId = returnModel.PayerId,
                Transactions = new List<CartBase>()
                {
                    new CartBase()
                    {
                        Amount = new Amount()
                        {
                            Currency = order.CurrencyCode,
                            Total = order.OrderTotal.ToString("N")
                        }
                    }
                }
            };
            var pcRequest = new PaymentExecuteRequest(returnModel.PaymentId);
            pcRequest.RequestBody(payment);

            var environment = GetEnvironment(settings);

            var client = new PayPalHttpClient(environment);
            var transactionResult = new TransactionResult();
            try
            {
                var response = client.Execute(pcRequest).Result;
                var result = response.Result<Payment>();
                transactionResult.Success = true;
                transactionResult.NewStatus = result.State == "approved" ? PaymentStatus.Complete : PaymentStatus.OnHold;
                transactionResult.OrderGuid = order.Guid;
                transactionResult.TransactionAmount = order.OrderTotal;
                transactionResult.TransactionGuid = returnModel.PaymentId;
                transactionResult.TransactionCurrencyCode = result.Transactions[0].Amount.Currency;
                transactionResult.ResponseParameters = new Dictionary<string, object>()
                {
                    { "id", result.Id },
                    { "payerId", returnModel.PayerId },
                    { "paymentId", returnModel.PaymentId },
                    { "createTime", result.CreateTime },
                    { "failureReason", result.FailureReason },
                    { "experienceProfileId", result.ExperienceProfileId },
                    { "noteToPayer", result.NoteToPayer },
                    { "intent", result.Intent },
                    { "state", result.State},
                    { "updateTime", result.UpdateTime },
                };
            }
            catch (BraintreeHttp.HttpException ex)
            {
                transactionResult.Success = false;
                transactionResult.Exception = ex;
            }

            return transactionResult;
        }

        public static string FetchWebProfile(PaypalWithRedirectSettings paypalSettings)
        {
            if (!paypalSettings.CheckoutProfileId.IsNullEmptyOrWhiteSpace())
                return paypalSettings.CheckoutProfileId;

            var webProfileRequest = new WebProfileCreateRequest();
            webProfileRequest.RequestBody(new WebProfile()
            {
                Temporary = false,
                InputFields = new InputFields()
                {
                    NoShipping = 1,
                    AddressOverride = 1
                },
                Name = Guid.NewGuid().ToString()
            });
            var environment = GetEnvironment(paypalSettings);
            var client = new PayPalHttpClient(environment);
            try
            {
                var response = client.Execute(webProfileRequest).Result;
                var result = response.Result<WebProfile>();
                var id = result.Id;
                paypalSettings.CheckoutProfileId = id;
                var settingService = DependencyResolver.Resolve<ISettingService>();
                settingService.Save(paypalSettings);
                return id;
            }
            catch (BraintreeHttp.HttpException ex)
            {
                return null;
            }
        }
    }
}