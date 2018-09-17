using System.Linq;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Services.Captures
{
    public class ShipmentCapture : IFoundationEntityUpdated<Shipment>
    {
        private readonly IPurchaseAccountant _purchaseAccountant;
        public ShipmentCapture(IPurchaseAccountant purchaseAccountant)
        {
            _purchaseAccountant = purchaseAccountant;
        }

        public void OnUpdated(Shipment entity)
        {
            var orderItems = entity.ShipmentItems.Select(x => x.OrderItem);
            foreach(var orderItem in orderItems)
                _purchaseAccountant.EvaluateOrderStatus(orderItem.Order);
        }

    }
}