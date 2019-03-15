using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Shipments;

namespace RoastedMarketplace.Factories.Shipments
{
    public interface IShipmentModelFactory : IModelFactory<Shipment, ShipmentModel>, IModelFactory<ShipmentItem, ShipmentItemModel>
    {
        
    }
}