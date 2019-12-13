using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Data.Entity.Payments
{
    public class TransactionResult
    {
        public string TransactionGuid { get; set; }

        public string OrderGuid { get; set; }

        public Order Order { get; set; }

        public bool Success { get; set; }

        public Exception Exception { get; set; }

        public PaymentStatus NewStatus { get; set; }

        public Dictionary<string, object> ResponseParameters { get; set; }

        public string RedirectionUrl { get; private set; }

        public bool RequiresRedirection { get; private set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionCurrencyCode { get; set; }

        public bool IsOfflineTransaction { get; set; }

        public bool IsSubscription { get; set; }

        public TransactionResult Redirect(string url)
        {
            RedirectionUrl = url;
            RequiresRedirection = true;
            return this;
        }
    }
}