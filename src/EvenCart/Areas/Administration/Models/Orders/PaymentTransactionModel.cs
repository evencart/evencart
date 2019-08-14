using System;
using EvenCart.Data.Entity.Payments;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class PaymentTransactionModel : FoundationEntityModel
    {
        public string TransactionGuid { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrderGuid { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionAmountFormatted => TransactionAmount.ToCurrency();

        public string UserIpAddress { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDisplay { get; set; }

        public string TransactionCodesSerialized { get; set; }

        public string TransactionCurrencyCode { get; set; }
    }
}