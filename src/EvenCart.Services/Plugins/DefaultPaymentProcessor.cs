using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Helpers;
using EvenCart.Services.Payments;

namespace EvenCart.Services.Plugins
{
    public class DefaultPaymentProcessor : IPaymentProcessor
    {
        public TransactionResult ProcessPayment(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            var paymentMethodName = order.PaymentMethodName;
            var paymentMethodInfo = PluginHelper.GetPaymentHandler(paymentMethodName);

            if (paymentMethodInfo == null)
                throw new Exception("Can't load payment method");

            //create transaction request
            var transactionRequest = new TransactionRequest()
            {
                Order = order,
                IsPartialRefund = false,
                TransactionGuid = Guid.NewGuid().ToString(),
                Parameters = paymentMethodData
            };

            var transactionResult = paymentMethodInfo.ProcessTransaction(transactionRequest);
            transactionResult.OrderGuid = order.Guid;
            return transactionResult;
        }
    }
}