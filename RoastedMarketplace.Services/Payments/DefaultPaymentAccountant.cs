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
        private readonly ICartService _cartService;
        public DefaultPaymentAccountant(ILogger logger, IOrderService orderService, IPaymentTransactionService paymentTransactionService, ICartService cartService)
        {
            _logger = logger;
            _orderService = orderService;
            _paymentTransactionService = paymentTransactionService;
            _cartService = cartService;
        }

        public void ProcessTransactionResult(TransactionResult result, bool clearCart = false)
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
                TransactionCurrencyCode = result.TransactionCurrencyCode
            };
            paymentTransaction.SetTransactionCodes(result.ResponseParameters);
            //save this
            _paymentTransactionService.Insert(paymentTransaction);

            if (order.CurrencyCode != result.TransactionCurrencyCode || order.PaymentStatus != result.NewStatus)
            {
                //update order
                order.CurrencyCode = result.TransactionCurrencyCode;
                order.PaymentStatus = result.NewStatus;
                _orderService.Update(order);
            }

            //clear cart
            if (clearCart)
                _cartService.ClearCart(order.UserId);
        }
    }
}