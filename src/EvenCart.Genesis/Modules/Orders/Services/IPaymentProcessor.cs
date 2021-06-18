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

using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Payments
{
    public interface IPaymentProcessor
    {
        TransactionResult ProcessPayment(Order order, decimal creditAmount, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessRefund(Order order, decimal amount, bool isPartial, RefundType refundType);

        TransactionResult ProcessVoid(Order order);

        TransactionResult ProcessCapture(Order order);

        TransactionResult ProcessCreateSubscription(Order order, decimal creditAmount, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessSubscription(Order order, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessCancelSubscription(Order order, Dictionary<string, object> paymentMethodData = null);
    }
}