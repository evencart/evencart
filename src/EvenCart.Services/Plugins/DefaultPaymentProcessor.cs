using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Helpers;
using EvenCart.Services.Payments;
using EvenCart.Services.Users;

namespace EvenCart.Services.Plugins
{
    public class DefaultPaymentProcessor : IPaymentProcessor
    {
        private readonly IPaymentTransactionService _paymentTransactionService;
        private readonly IStoreCreditService _storeCreditService;
        public DefaultPaymentProcessor(IPaymentTransactionService paymentTransactionService, IStoreCreditService storeCreditService)
        {
            _paymentTransactionService = paymentTransactionService;
            _storeCreditService = storeCreditService;
        }

        public TransactionResult ProcessPayment(Order order, decimal creditAmount, Dictionary<string, object> paymentMethodData = null)
        {
            //if store credits are available, they must be deducted from the amount being processed by payment processor
            var amount = order.OrderTotal - creditAmount;
            if (creditAmount > 0)
            {
                //lock these many credits so they can't be used till this transaction is complete
                _storeCreditService.LockCredits(order.StoreCredits, order.UserId);
            }
            return ProcessResult(order, TransactionRequestType.Payment, amount, false, paymentMethodData);
        }

        public TransactionResult ProcessRefund(Order order, decimal amount, bool isPartial, RefundType refundType)
        {
            return ProcessResult(order, TransactionRequestType.Refund, amount, isPartial);
        }

        public TransactionResult ProcessVoid(Order order)
        {
            var transaction = _paymentTransactionService.FirstOrDefault(x => x.OrderGuid == order.Guid && x.PaymentStatus == PaymentStatus.Authorized);
            return ProcessResult(order, TransactionRequestType.Void, order.OrderTotal,
                paymentMethodData: transaction.TransactionCodes);
        }

        public TransactionResult ProcessCapture(Order order)
        {
            var transaction = _paymentTransactionService.FirstOrDefault(x => x.OrderGuid == order.Guid && x.PaymentStatus == PaymentStatus.Authorized);
            return ProcessResult(order, TransactionRequestType.Capture, order.OrderTotal,
                paymentMethodData: transaction.TransactionCodes);
        }

        public TransactionResult ProcessCreateSubscription(Order order, decimal creditAmount, Dictionary<string, object> paymentMethodData = null)
        {
            var amount = order.OrderTotal - creditAmount;
            return ProcessResult(order, TransactionRequestType.SubscriptionCreate, amount, false, paymentMethodData);
        }

        public TransactionResult ProcessSubscription(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            return ProcessResult(order, TransactionRequestType.Subscription, order.OrderTotal, false, paymentMethodData);
        }

        public TransactionResult ProcessCancelSubscription(Order order, Dictionary<string, object> paymentMethodData = null)
        {
            return ProcessResult(order, TransactionRequestType.SubscriptionCancel, null, false, paymentMethodData);
        }

        private TransactionResult ProcessResult(Order order, TransactionRequestType requestType, decimal? amount, bool isPartialRefund = false, Dictionary<string, object> paymentMethodData = null)
        {
            var paymentMethodName = order.PaymentMethodName;
            var paymentMethodInfo = PluginHelper.GetPaymentHandler(paymentMethodName);

            if (paymentMethodInfo == null)
            {
                if (order.UsedStoreCredits)
                {
                    if (amount <= 0)
                        return new TransactionResult()
                        {
                            Order = order,
                            TransactionGuid = Guid.NewGuid().ToString(),
                            ResponseParameters = paymentMethodData,
                            Success = true,
                            NewStatus = PaymentStatus.Complete,
                            TransactionAmount = order.OrderTotal
                        };
                }
                throw new Exception("Can't load payment method");
            }

            switch (requestType)
            {
                case TransactionRequestType.Capture when !paymentMethodInfo.SupportedOperations.Contains(PaymentOperation.Capture):
                case TransactionRequestType.Void when !paymentMethodInfo.SupportedOperations.Contains(PaymentOperation.Void):
                case TransactionRequestType.Refund when !paymentMethodInfo.SupportedOperations.Contains(PaymentOperation.Refund):
                    return new TransactionResult()
                    {
                        Success = false
                    };
            }

            //create transaction request
            var transactionRequest = new TransactionRequest()
            {
                Order = order,
                IsPartialRefund = isPartialRefund,
                TransactionGuid = Guid.NewGuid().ToString(),
                Parameters = paymentMethodData,
                Amount = amount,
                RequestType = requestType
            };

            var transactionResult = paymentMethodInfo.ProcessTransaction(transactionRequest);
            transactionResult.OrderGuid = order.Guid;
            transactionResult.Order = order;
            return transactionResult;
        }
    }
}