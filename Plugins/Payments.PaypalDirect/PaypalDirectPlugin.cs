using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Services.Payments;
using RoastedMarketplace.Services.Plugins;

namespace Payments.PaypalDirect
{
    public class PaypalDirectPlugin : FoundationPlugin, IPaymentHandlerPlugin
    {

        public PaymentMethodType PaymentMethodType => PaymentMethodType.CreditCard;

        public string PaymentHandlerComponentRouteName => PaypalDirectConfig.PaymentHandlerComponentRouteName;

        public PaymentOperation[] SupportedOperations => new[]
            {PaymentOperation.Authorize, PaymentOperation.Capture, PaymentOperation.Refund, PaymentOperation.Void};

        public TransactionResult ProcessTransaction(TransactionRequest request)
        {
            /*
            var order = request.Order;
            var customer = order.User;
            var billingAddress = order.BillingAddress;
            var payer = new Payer()
            {
                payment_method = "credit_card",
                funding_instruments = new List<FundingInstrument>()
                {
                    new FundingInstrument()
                    {
                        credit_card = new CreditCard()
                        {
                            billing_address = new Address()
                            {
                                city = billingAddress.City,
                                country_code = billingAddress.Country.Code,
                                line1 = billingAddress.Address1,
                                postal_code = billingAddress.ZipPostalCode,
                                state = billingAddress.StateOrProvince.Name
                            },
                            cvv2 = request.Parameters["cvv"].ToString(),
                            expire_month = 11,
                            expire_year = 2018,
                            first_name = customer.FirstName,
                            last_name = customer.LastName,
                            number = request.Parameters["card_number"].ToString(),
                            type = "visa"
                        }
                    }
                },
                payer_info = new PayerInfo {
                    email = "test@email.com"
                }
            };*/
            return new TransactionResult() { Success = true };
        }

        public decimal GetPaymentHandlerFee(Cart cart)
        {
            return 0;
        }

        public bool ValidatePaymentInfo(Dictionary<string, string> parameters, out string error)
        {

            error = null;
            //extract all the values
            parameters.TryGetValue("cardNumber", out string cardNumber);
            parameters.TryGetValue("nameOnCard", out string nameOnCard);
            parameters.TryGetValue("expiryMonth", out string expiryMonthStr);
            parameters.TryGetValue("expiryYear", out string expiryYearStr);
            parameters.TryGetValue("cvv", out string cvv);

            var suppliedDataInvalid = cardNumber.IsNullEmptyOrWhitespace() || nameOnCard.IsNullEmptyOrWhitespace() ||
                                expiryMonthStr.IsNullEmptyOrWhitespace() || expiryYearStr.IsNullEmptyOrWhitespace() ||
                                cvv.IsNullEmptyOrWhitespace();
            if (suppliedDataInvalid)
            {
                error = LocalizationHelper.Localize("All fields are required", ApplicationEngine.CurrentLanguageCultureCode);
                return false;
            }
            //get the raw number
            cardNumber = GetRawCreditNumber(cardNumber);

            var cardNumberValid = Mod10Check(cardNumber);
            if (!cardNumberValid)
            {
                error = LocalizationHelper.Localize("Invalid card number supplied", ApplicationEngine.CurrentLanguageCultureCode);
                return false;
            }

            var expiryDateValid = ExpiryCheck(expiryMonthStr, expiryYearStr);
            if (!expiryDateValid)
            {
                error = LocalizationHelper.Localize("The card is no longer valid", ApplicationEngine.CurrentLanguageCultureCode);
                return false;
            }
            //if we are here, the data supplied is valid in it's format. We don't know if it's actually valid until we process it down the line
            //so return affirmative for now
            return true;
        }
        

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
