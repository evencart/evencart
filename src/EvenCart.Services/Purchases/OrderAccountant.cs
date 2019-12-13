using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Payments;
using EvenCart.Services.Products;
using EvenCart.Services.Tokens;

namespace EvenCart.Services.Purchases
{
    public class OrderAccountant : IOrderAccountant
    {
        private readonly IWarehouseInventoryService _warehouseInventoryService;
        private readonly IOrderFulfillmentService _orderFulfillmentService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly OrderSettings _orderSettings;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IPaymentProcessor _paymentProcessor;
        public OrderAccountant(IWarehouseInventoryService warehouseInventoryService, IOrderFulfillmentService orderFulfillmentService, ITokenGenerator tokenGenerator, OrderSettings orderSettings, IOrderService orderService, IOrderItemService orderItemService, IPaymentProcessor paymentProcessor)
        {
            _warehouseInventoryService = warehouseInventoryService;
            _orderFulfillmentService = orderFulfillmentService;
            _tokenGenerator = tokenGenerator;
            _orderSettings = orderSettings;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _paymentProcessor = paymentProcessor;
        }
        /// <summary>
        /// Gets order fulfillments based on the following rules.
        /// - If all the products in an order are present in multiple warehouses, select a warehouse that has lowest order and can fulfill all the products
        /// - If all the products in an order are present in only one of the warehouses, select that warehouse irrespective of order
        /// - If some products are present in one warehouse and others are present in other warehouse, apply warehouse selection based on their order for each product.
        /// </summary>
        private IList<OrderFulfillment> GetAutoOrderFulfillments(Order order, OrderFulfillmentType orderFulfillmentType)
        {
            //the product ids for this order
            var productIds = order.OrderItems.Select(x => x.ProductId).Distinct().ToList();
            var warehouseInventories = _warehouseInventoryService.GetByProducts(productIds).ToList();
            var availableWarehouses = warehouseInventories.Select(x => x.Warehouse).Distinct().OrderBy(x => x.DisplayOrder).ToList();
            var fulfillments = new List<OrderFulfillment>();
            foreach (var warehouse in availableWarehouses)
            {
                var wInventories = warehouseInventories.Where(x => x.WarehouseId == warehouse.Id).ToList();
                var warehouseCheckSuccess = true;
                foreach (var orderItem in order.OrderItems)
                {
                    if (fulfillments.Any(x => x.OrderItemId == orderItem.Id && x.Quantity == orderItem.Quantity))
                        continue; //do we already have fulfilled this item, yes and so skip the item now

                    WarehouseInventory warehouseInventory;
                    if (orderItem.ProductVariantId > 0)
                    {
                        warehouseInventory = wInventories.FirstOrDefault(x =>
                            x.ProductVariantId == orderItem.ProductVariantId &&
                            x.AvailableQuantity >= orderItem.Quantity);

                        //do we have any inventory for this variant in this warehouse, if not, we don't need to proceed
                        if (warehouseInventory == null)
                        {
                            if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                            {
                                warehouseCheckSuccess = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        warehouseInventory = wInventories.FirstOrDefault(x =>
                            x.ProductId == orderItem.ProductId &&
                            x.AvailableQuantity >= orderItem.Quantity);
                        //do we have any inventory for this product in this warehouse, if not, we don't need to proceed
                        if (warehouseInventory == null)
                        {
                            if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                            {
                                warehouseCheckSuccess = false;
                                break;
                            }
                        }
                    }
                    //add only if fulfillment can be done
                    if (warehouseInventory?.AvailableQuantity >= orderItem.Quantity)
                    {
                        fulfillments.Add(new OrderFulfillment()
                        {
                            Warehouse = warehouse,
                            Order = order,
                            OrderItem = orderItem,
                            OrderId = order.Id,
                            OrderItemId = orderItem.Id,
                            Quantity = orderItem.Quantity,
                            WarehouseId = warehouse.Id,
                            Verified = false,
                            WarehouseInventory = warehouseInventory
                        });
                    }
                    else
                    {
                        warehouseCheckSuccess = false;
                        break;
                    }
                }

                if (!warehouseCheckSuccess)
                {
                    if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                        fulfillments.Clear(); //avoid the fulfillment by this warehouse
                }
                else
                {
                    return fulfillments; //we got the warehouse, so return
                }
            }

            return fulfillments;
        }

        public IList<OrderFulfillment> GetAutoOrderFulfillments(Order order)
        {
            foreach (var fulfillmentType in new List<OrderFulfillmentType>()
            {
                OrderFulfillmentType.WholeFromSingleWarehouse,
                OrderFulfillmentType.SplitToMultipleWarehouseByItem,
                OrderFulfillmentType.SplitToMultipleWarehouseByItemQuantity
            })
            {
                var fulfillment = GetAutoOrderFulfillments(order, fulfillmentType);
                if (fulfillment.Any())
                    return fulfillment;
            }

            return null;
        }

        public IList<OrderFulfillment> SaveAutOrderFulfillments(Order order)
        {
            var fulfillments = GetAutoOrderFulfillments(order);
            if (fulfillments != null)
            {
                Transaction.Initiate(transaction =>
                {
                    foreach (var fulfillment in fulfillments)
                    {
                        fulfillment.WarehouseInventory.ReservedQuantity += fulfillment.Quantity;
                        //update inventory
                        _warehouseInventoryService.Update(fulfillment.WarehouseInventory, transaction);
                        _orderFulfillmentService.Insert(fulfillment, transaction);
                    }
                });
            }

            return fulfillments;
        }

        public void InsertCompleteOrder(Order order)
        {
            //save the order now
            _orderService.Insert(order);

            Transaction.Initiate(transaction =>
            {
                //save the order items
                foreach (var oi in order.OrderItems)
                {
                    oi.OrderId = order.Id;
                    _orderItemService.Insert(oi, transaction);
                }
            });

            //block the inventories
            SaveAutOrderFulfillments(order);

            //generate order number & update it
            var orderNumber = _tokenGenerator.MakeToken(new TemplateToken()
            {
                DateTime = order.CreatedOn,
                Id = order.Id,
                Template = _orderSettings.OrderNumberTemplate,
                UserId = order.UserId
            });
            order.OrderNumber = orderNumber;
            _orderService.Update(order);
        }

        public void CancelOrder(Order order, string cancellationReason, bool finalize = false)
        {
            //get the fulfillments
            var fulfillments = _orderFulfillmentService.Get(x => x.OrderId == order.Id).ToList();
            var warehouseIds = fulfillments.Select(x => x.WarehouseId).ToList();
            var productIds = fulfillments.Select(x => x.OrderItem.ProductId).ToList();
            var productVariantIds = fulfillments.Select(x => x.OrderItem.ProductVariantId).ToList();
            var warehouseInventories = _warehouseInventoryService.Get(x =>
                warehouseIds.Contains(x.WarehouseId) &&
                (productVariantIds.Contains((int)x.ProductVariantId) || productIds.Contains(x.ProductId))).ToList();

            Transaction.Initiate(transaction =>
            {
                foreach (var fulfillment in fulfillments)
                {
                    var inventory = fulfillment.OrderItem.ProductVariantId > 0
                        ? warehouseInventories.FirstOrDefault(x =>
                            x.ProductVariantId == fulfillment.OrderItem.ProductVariantId)
                        : warehouseInventories.FirstOrDefault(x => x.ProductId == fulfillment.OrderItem.ProductId);

                    if (inventory == null)
                        continue;
                    inventory.ReservedQuantity -= fulfillment.Quantity; //clear from reserve
                    _warehouseInventoryService.Update(inventory, transaction);
                    _orderFulfillmentService.Delete(fulfillment, transaction);
                }

                order.OrderStatus = order.OrderTotal > 0 && !finalize
                    ? OrderStatus
                        .PendingCancellation /*admin will cancel manually and refund or do whatever needs to be done*/
                    : OrderStatus.Cancelled;
                if (order.Remarks.IsNullEmptyOrWhiteSpace())
                    order.Remarks = $"Cancellation Reason : {cancellationReason}";
                else
                {
                    order.Remarks = order.Remarks + Environment.NewLine + $"Cancellation Reason : {cancellationReason}"; ;
                }
                _orderService.Update(order, transaction);
            });
        }

        public Order CloneOrder(Order order)
        {
            var clonedOrder = ObjectHelper.Clone(order);
            //reset the identifiers
            clonedOrder.Id = 0;
            clonedOrder.Guid = Guid.NewGuid().ToString();
            clonedOrder.CreatedOn = DateTime.UtcNow;
            clonedOrder.OrderStatus = OrderStatus.New;
            foreach (var oi in clonedOrder.OrderItems)
            {
                oi.Id = 0;
            }
            return clonedOrder;
        }
    }
}