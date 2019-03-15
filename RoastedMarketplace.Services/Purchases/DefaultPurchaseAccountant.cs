using System;
using System.Linq;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Logger;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Services.Purchases
{
    public class DefaultPurchaseAccountant : IPurchaseAccountant
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        public DefaultPurchaseAccountant(IOrderService orderService, ILogger logger, IProductService productService)
        {
            _orderService = orderService;
            _logger = logger;
            _productService = productService;
        }

        public void EvaluateOrderStatus(Order order)
        {
            if (order == null)
            {
                _logger.LogError<Order>(new ArgumentNullException(nameof(Order)), "Can't update a null order");
                return;
            }
            //if order is complete already, give up
            if (order.OrderStatus == OrderStatus.Complete)
                return;

            //we can't do anything to update order status for now
            if (order.Shipments == null)
            {
                //we'll fetch all the products and see if there is any shippable
                var productIds = order.OrderItems.Select(x => x.ProductId).ToList();
                var products = _productService.GetProducts(productIds);
                if (products.All(x => !x.IsShippable))
                {
                    //no product is shippable, was payment done?
                    order.OrderStatus = order.PaymentStatus == PaymentStatus.Complete ? OrderStatus.Complete : OrderStatus.Processing;
                    _orderService.Update(order);
                }
                return;
            }
            var orderStatus = order.OrderStatus;
            var orderItemIds = order.OrderItems.Select(x => x.Id).ToList();
            var shippedOrderItemIds = order.Shipments.Where(x => x.ShipmentStatus == ShipmentStatus.InTransit || x.ShipmentStatus == ShipmentStatus.Packaged)
                .SelectMany(x => x.ShipmentItems.Select(y => y.OrderItemId))
                .ToList();
            if (orderItemIds.Count > shippedOrderItemIds.Count)
                orderStatus = OrderStatus.PartiallyShipped;
            else
            {
                //check if there is partial shipment for individual item
                var shippedOrderItemsCount = order.Shipments.SelectMany(x => x.ShipmentItems)
                    .GroupBy(x => x.OrderItemId, x => x.Quantity)
                    .ToList();
                foreach (var oic in shippedOrderItemsCount)
                {
                    var orderItemId = oic.Key;
                    var shippedCount = oic.Sum();
                    //if any individual items is short in supply then mark as partially shipped
                    if (order.OrderItems.First(x => x.Id == orderItemId).Quantity > shippedCount)
                    {
                        orderStatus = OrderStatus.PartiallyShipped;
                        break;
                    }
                }
            }

            //has the order status changed, if not check further for other possibilities
            if (order.OrderStatus == orderStatus || orderStatus == OrderStatus.PartiallyShipped)
            {
                if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Packaged))
                    orderStatus = OrderStatus.Processing;
                else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Returned))
                    orderStatus = OrderStatus.Returned;
                else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.InTransit) && orderStatus != OrderStatus.PartiallyShipped)
                    orderStatus = OrderStatus.Shipped;
                else if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.OutForDelivery) && orderStatus != OrderStatus.PartiallyShipped)
                    orderStatus = OrderStatus.Shipped;
                else if (order.Shipments.Any(x => x.ShipmentStatus == ShipmentStatus.InTransit))
                    orderStatus = OrderStatus.PartiallyShipped;

                if (order.PaymentStatus == PaymentStatus.Complete)
                {
                    if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Delivered))
                        orderStatus = OrderStatus.Complete;
                }
                else
                {
                    if (order.Shipments.All(x => x.ShipmentStatus == ShipmentStatus.Delivered))
                        orderStatus = OrderStatus.OnHold; //COD may be?
                }
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