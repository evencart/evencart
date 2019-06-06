using System;
using System.Linq;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using EvenCart.Services.Shipping;

namespace EvenCart.Services.Captures
{
    public class ShipmentCapture : IFoundationEntityUpdated<Shipment>
    {
        private readonly IPurchaseAccountant _purchaseAccountant;
        private readonly IShipmentStatusHistoryService _shipmentStatusHistoryService;
        private readonly IOrderFulfillmentService _orderFulfillmentService;
        private readonly IWarehouseInventoryService _warehouseInventoryService;
        public ShipmentCapture(IPurchaseAccountant purchaseAccountant, IShipmentStatusHistoryService shipmentStatusHistoryService, IOrderFulfillmentService orderFulfillmentService, IWarehouseInventoryService warehouseInventoryService)
        {
            _purchaseAccountant = purchaseAccountant;
            _shipmentStatusHistoryService = shipmentStatusHistoryService;
            _orderFulfillmentService = orderFulfillmentService;
            _warehouseInventoryService = warehouseInventoryService;
        }

        public void OnUpdated(Shipment entity)
        {
            var orderItems = entity.ShipmentItems.Select(x => x.OrderItem).ToList();
            foreach(var orderItem in orderItems)
                _purchaseAccountant.EvaluateOrderStatus(orderItem.Order);

            //if the shipment status is InTransit/Returned, that means we should update the inventory, as the item has been shipped/returned
            if (entity.ShipmentStatus == ShipmentStatus.InTransit || entity.ShipmentStatus == ShipmentStatus.Returned)
            {
                var warehouseId = entity.WarehouseId;
                var shippedProductIds = orderItems.Select(x => x.ProductId).ToList();
                var inventories = _warehouseInventoryService.GetByProducts(shippedProductIds, warehouseId).ToList();
                foreach (var shippedItem in entity.ShipmentItems)
                {
                    var quantityShipped = shippedItem.Quantity;
                    var shippedProductId = shippedItem.OrderItem.ProductId;
                    var variantId = shippedItem.OrderItem.ProductVariantId;
                    var inventory = shippedItem.OrderItem.ProductVariantId > 0
                        ? inventories.FirstOrDefault(x => x.ProductVariantId == variantId)
                        : inventories.FirstOrDefault(x => x.ProductId == shippedProductId);
                    if (inventory == null)
                        continue; //weird it's not there. but we can't do anything for this. something is wrong. this shouldn't have hit
                    if (entity.ShipmentStatus == ShipmentStatus.InTransit)
                    {
                        inventory.ReservedQuantity -= quantityShipped;
                        inventory.TotalQuantity -= quantityShipped;
                    }
                    else
                    {
                        inventory.TotalQuantity += quantityShipped;
                    }
                    _warehouseInventoryService.Update(inventory);
                }
            }

            //update shipment history
            //get the history
            var shipmentHistoryItems = _shipmentStatusHistoryService.Get(x => x.ShipmentId == entity.Id).OrderByDescending(x => x.Id).ToList();
            //if the current status is already there as the latest one, no need to do anything, 
            if (shipmentHistoryItems.Any() && shipmentHistoryItems.First().ShipmentStatus ==
                entity.ShipmentStatus) return;
            //we'll add this to the history now
            var shipmentHistory = new ShipmentHistory()
            {
                CreatedOn = DateTime.UtcNow,
                ShipmentStatus = entity.ShipmentStatus,
                ShipmentId = entity.Id
            };
            _shipmentStatusHistoryService.Insert(shipmentHistory);
        }

    }
}