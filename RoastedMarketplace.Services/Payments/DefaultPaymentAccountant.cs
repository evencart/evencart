using System;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Logger;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Services.Payments
{
    public class DefaultPaymentAccountant : IPaymentAccountant
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private readonly IPaymentTransactionService _paymentTransactionService;
        private readonly IPurchaseAccountant _purchaseAccountant;
        public DefaultPaymentAccountant(ILogger logger, IOrderService orderService, IPaymentTransactionService paymentTransactionService, IPurchaseAccountant purchaseAccountant)
        {
            _logger = logger;
            _orderService = orderService;
            _paymentTransactionService = paymentTransactionService;
            _purchaseAccountant = purchaseAccountant;
        }

        public void ProcessTransactionResult(TransactionResult result)
        {
            var order = _orderService.GetByGuid(result.OrderGuid);
            if (!result.Success)
            {
                _logger.LogError<Order>(result.Exception, "Error occured while processing payment", order.User, result.ResponseParameters);
                return;
            }
            var paymentTransaction = new PaymentTransaction()
            {
                CreatedOn = DateTime.UtcNow,
                OrderGuid = order.Guid,
                PaymentMethodName = order.PaymentMethodName,
                PaymentStatus = result.NewStatus,
                UserIpAddress = order.UserIpAddress,
                TransactionAmount = result.TransactionAmount,
                TransactionGuid = result.TransactionGuid,
                TransactionCodes = result.ResponseParameters,
                TransactionCurrencyCode = result.TransactionCurrencyCode
            };
            //save this
            _paymentTransactionService.Insert(paymentTransaction);

            //update order
            order.CurrencyCode = result.TransactionCurrencyCode;
            _orderService.Update(order);
        }
    }
}