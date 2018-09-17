using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Services.Captures
{
    public class PaymentTransactionCapture : IFoundationEntityInserted<PaymentTransaction>
    {
        private readonly IPurchaseAccountant _purchaseAccountant;
        private readonly IOrderService _orderService;
        public PaymentTransactionCapture(IPurchaseAccountant purchaseAccountant, IOrderService orderService)
        {
            _purchaseAccountant = purchaseAccountant;
            _orderService = orderService;
        }

        public void OnInserted(PaymentTransaction entity)
        {
            //get the order
            var order = _orderService.GetByGuid(entity.OrderGuid);
            if (order == null)
                return; //do nothing, we don't have the order

            order.PaymentStatus = entity.PaymentStatus;
            _purchaseAccountant.EvaluateOrderStatus(order);
        }
    }
}