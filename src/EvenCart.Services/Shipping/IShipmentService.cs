using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Shipping
{
    public interface IShipmentService : IFoundationEntityService<Shipment>
    {
        void RemoveShipmentItem(int shipmentItemId);

        IList<Shipment> GetShipmentsByOrderId(int orderId);
    }
}