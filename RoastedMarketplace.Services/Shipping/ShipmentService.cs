using System.Linq;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Shipping
{
    public class ShipmentService : FoundationEntityService<Shipment>, IShipmentService
    {
        public ShipmentItem AddShipmentItem(int shipmentId, int orderItemId)
        {
            var shipmentItem = new ShipmentItem()
            {
                OrderItemId = orderItemId,
                ShipmentId = shipmentId
            };
            EntitySet<ShipmentItem>.Insert(shipmentItem);
            return shipmentItem;
        }

        public void RemoveShipmentItem(int shipmentItemId)
        {
            EntitySet<ShipmentItem>.Delete(x => x.Id == shipmentItemId);
        }

        public override Shipment Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<ShipmentItem>("Id", "ShipmentId", joinType: JoinType.LeftOuter)
                .Join<OrderItem>("OrderItemId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Shipment, ShipmentItem>())
                .Relate<OrderItem>((shipment, item) =>
                {
                    var shipmentItem = shipment.ShipmentItems.FirstOrDefault(x => x.OrderItemId == item.Id);
                    if (shipmentItem != null)
                        shipmentItem.OrderItem = item;
                })
                .SelectNested()
                .FirstOrDefault();
        }
    }
}