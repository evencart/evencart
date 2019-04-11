using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Shipments;

namespace EvenCart.Factories.Shipments
{
    public interface IShipmentModelFactory : IModelFactory<Shipment, ShipmentModel>, IModelFactory<ShipmentItem, ShipmentItemModel>
    {
        
    }
}