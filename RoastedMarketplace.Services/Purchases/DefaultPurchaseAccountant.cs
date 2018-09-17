using System;
using System.Linq;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Logger;

namespace RoastedMarketplace.Services.Purchases
{
    public class DefaultPurchaseAccountant : IPurchaseAccountant
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        public DefaultPurchaseAccountant(IOrderService orderService, ILogger logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public void EvaluateOrderStatus(Order order)
        {
            if (order == null)
            {
                _logger.LogError<Order>(new ArgumentNullException(nameof(Order)), null, "Can't update a null order");
                return;
            }
            //if order is complete already, give up
            if (order.OrderStatus == OrderStatus.Complete)
                return;

            var orderStatus = order.OrderStatus;
            var orderItemIds = order.OrderItems.Select(x => x.Id);
            var shippedOrderItemIds = order.Shipments.Where(x => x.ShipmentStatus == ShipmentStatus.InTransit)
                .SelectMany(x => x.ShipmentItems.Select(y => y.OrderItemId))
                .ToList();

            if (orderItemIds.Count() > shippedOrderItemIds.Count)
                orderStatus = OrderStatus.PartiallyShipped;
            else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Returned))
                orderStatus = OrderStatus.Returned;
            else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.InTransit))
                orderStatus = OrderStatus.Shipped;
            else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Packaged))
                orderStatus = OrderStatus.Processing;

            if (order.PaymentStatus == PaymentStatus.Complete)
            {
                if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Delivered))
                    orderStatus = OrderStatus.Complete;
            }
            else
            {
                if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Delivered))
                    orderStatus = OrderStatus.OnHold;
            }

            //update the order status to db
            if (order.OrderStatus != orderStatus)
            {
                order.OrderStatus = orderStatus;
                _orderService.Update(order);
            }
        }

        public void EvaluateOrderStatus(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            EvaluateOrderStatus(order);
        }
    }
}