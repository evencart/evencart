using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Shipping
{
    public interface IShipmentService : IFoundationEntityService<Shipment>
    {
        ShipmentItem AddShipmentItem(int shipmentId, int orderItemId);

        void RemoveShipmentItem(int shipmentItemId);
    }
}