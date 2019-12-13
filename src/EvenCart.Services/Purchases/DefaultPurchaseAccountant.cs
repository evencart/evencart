using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Extensions;
using EvenCart.Services.Helpers;
using EvenCart.Services.Logger;
using EvenCart.Services.Products;

namespace EvenCart.Services.Purchases
{
    public class DefaultPurchaseAccountant : IPurchaseAccountant
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly IReturnRequestService _returnRequestService;
        public DefaultPurchaseAccountant(IOrderService orderService, ILogger logger, IProductService productService, IReturnRequestService returnRequestService)
        {
            _orderService = orderService;
            _logger = logger;
            _productService = productService;
            _returnRequestService = returnRequestService;
        }

        public void EvaluateOrderStatus(Order order)
        {
            if (order == null)
            {
                _logger.LogError<Order>(new ArgumentNullException(nameof(Order)), "Can't update a null order");
                return;
            }

            if (new List<OrderStatus> { OrderStatus.Cancelled, OrderStatus.Returned, OrderStatus.PartiallyReturned, OrderStatus.Closed, OrderStatus.SubscriptionCancelled }.Contains(order.OrderStatus))
                return;
            //if order is complete already, give up
            if (order.OrderStatus == OrderStatus.Complete)
            {
                var returnRequests = _returnRequestService.GetOrderReturnRequests(order.Id).ToList();
                if (returnRequests.Any(x => x.ReturnRequestStatus != ReturnRequestStatus.Complete))
                    return; //do nothing
                if (returnRequests.Count < order.OrderItems.Count ||
                    returnRequests.Sum(x => x.Quantity) < order.OrderItems.Sum(x => x.Quantity))
                    order.OrderStatus = OrderStatus.PartiallyReturned;
                else
                    order.OrderStatus = OrderStatus.Returned;
                _orderService.Update(order);
                return;
            }
            //is that an order for digital products only
            if (order.PaymentStatus == PaymentStatus.Complete && OrderHelper.IsDownloadOnly(order))
            {
                //the order can be marked complete here
                order.OrderStatus = OrderStatus.Complete;
                _orderService.Update(order);
                return;
            }
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
                //depending on the order status, we'll update the popularity
                var productIds = order.OrderItems.Select(x => x.ProductId).ToArray();
                switch (orderStatus)
                {
                    case OrderStatus.Complete:
                        _productService.UpdatePopularityIndex(true, productIds);
                        break;
                    case OrderStatus.Returned:
                        _productService.UpdatePopularityIndex(false, productIds);
                        break;
                }
            }
        }

        public void EvaluateOrderStatus(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            EvaluateOrderStatus(order);
        }
    }
}