using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public interface IShipmentModelFactory : IModelFactory<Shipment, ShipmentModel>
    {
        IList<ShipmentEditorModel> Create(IList<Shipment> shipments);
    }
}