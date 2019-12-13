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
            return ProcessResult(order, TransactionRequestType.Payment, order.OrderTotal, paymentMethodData);
        }

        public TransactionResult ProcessRefund(Order order, decimal amount)
        {
            return ProcessResult(order, TransactionRequestType.Refund, amount);
        }

        public TransactionResult ProcessVoid(Order order)
        {
            return ProcessResult(order, TransactionRequestType.Void, order.OrderTotal);
        }

        public TransactionResult ProcessCreateSubscription(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            return ProcessResult(order, TransactionRequestType.SubscriptionCreate, order.OrderTotal, paymentMethodData);
        }

        public TransactionResult ProcessSubscription(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            return ProcessResult(order, TransactionRequestType.Subscription, order.OrderTotal, paymentMethodData);
        }

        public TransactionResult ProcessCancelSubscription(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            return ProcessResult(order, TransactionRequestType.SubscriptionCancel, null, paymentMethodData);
        }

        private TransactionResult ProcessResult(Order order, TransactionRequestType requestType, decimal? amount, Dictionary<string, object> paymentMethodData = null)
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
                Parameters = paymentMethodData,
                Amount = amount,
                RequestType = requestType
            };

            var transactionResult = paymentMethodInfo.ProcessTransaction(transactionRequest);
            transactionResult.OrderGuid = order.Guid;
            return transactionResult;
        }
    }
}