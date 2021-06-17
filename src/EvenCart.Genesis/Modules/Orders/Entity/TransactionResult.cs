#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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

        public bool IsStoreCreditTransaction { get; set; }

        public bool IsSubscription { get; set; }

        public TransactionResult Redirect(string url)
        {
            RedirectionUrl = url;
            RequiresRedirection = true;
            return this;
        }
    }
}