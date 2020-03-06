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