using System;
using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Payments;

namespace RoastedMarketplace.Services.Plugins
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