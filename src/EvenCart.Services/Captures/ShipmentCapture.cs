using System;
using System.Linq;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Purchases;
using EvenCart.Services.Shipping;

namespace EvenCart.Services.Captures
{
    public class ShipmentCapture : IFoundationEntityUpdated<Shipment>
    {
        private readonly IPurchaseAccountant _purchaseAccountant;
        private readonly IShipmentStatusHistoryService _shipmentStatusHistoryService;
        public ShipmentCapture(IPurchaseAccountant purchaseAccountant, IShipmentStatusHistoryService shipmentStatusHistoryService)
        {
            _purchaseAccountant = purchaseAccountant;
            _shipmentStatusHistoryService = shipmentStatusHistoryService;
        }

        public void OnUpdated(Shipment entity)
        {
            var orderItems = entity.ShipmentItems.Select(x => x.OrderItem);
            foreach(var orderItem in orderItems)
                _purchaseAccountant.EvaluateOrderStatus(orderItem.Order);

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