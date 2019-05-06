using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Shipping
{
    public class ShipmentService : FoundationEntityService<Shipment>, IShipmentService
    {
        public ShipmentItem AddShipmentItem(int shipmentId, int orderItemId, int quantity)
        {
            var shipmentItem = new ShipmentItem()
            {
                OrderItemId = orderItemId,
                ShipmentId = shipmentId,
                Quantity = quantity
            };
            EntitySet<ShipmentItem>.Insert(shipmentItem);
            return shipmentItem;
        }

        public void RemoveShipmentItem(int shipmentItemId)
        {
            EntitySet<ShipmentItem>.Delete(x => x.Id == shipmentItemId);
        }

        public IList<Shipment> GetShipmentsByOrderId(int orderId)
        {
            Expression<Func<OrderItem, bool>> whereOrderIdMatches = item => item.OrderId == orderId;
            return Repository.Join<ShipmentItem>("Id", "ShipmentId", joinType: JoinType.LeftOuter)
                .Join<OrderItem>("OrderItemId", "Id", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<ShipmentHistory>("Id", "ShipmentId", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Shipment, ShipmentItem>())
                .Relate(RelationTypes.OneToMany<Shipment, ShipmentHistory>())
                .Relate<OrderItem>((shipment, item) =>
                {
                    if (shipment.ShipmentItems == null)
                        return;
                    var shipmentItem = shipment.ShipmentItems.FirstOrDefault(x => x.OrderItemId == item.Id);
                    if (shipmentItem != null)
                        shipmentItem.OrderItem = item;
                })
                .Relate<Product>((shipment, product) =>
                {
                    if (shipment.ShipmentItems == null)
                        return;
                    var shipmentItem =
                        shipment.ShipmentItems.FirstOrDefault(
                            x => x.OrderItem != null && x.OrderItem.ProductId == product.Id);
                    shipmentItem.OrderItem.Product = product;

                })
                .Where(whereOrderIdMatches)
                .SelectNested()
                .ToList();
        }

        public override Shipment Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<ShipmentItem>("Id", "ShipmentId", joinType: JoinType.LeftOuter)
                .Join<OrderItem>("OrderItemId", "Id", joinType: JoinType.LeftOuter)
                .Join<Order>("OrderId", "Id", joinType: JoinType.LeftOuter)
                .Join<ShipmentHistory>("Id", "ShipmentId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<User>("UserId", "Id", typeof(Order), JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Shipment, ShipmentItem>())
                .Relate(RelationTypes.OneToMany<Shipment, ShipmentHistory>())
                .Relate<OrderItem>((shipment, item) =>
                {
                    var shipmentItem = shipment.ShipmentItems.FirstOrDefault(x => x.OrderItemId == item.Id);
                    if (shipmentItem != null)
                        shipmentItem.OrderItem = item;
                })
                .Relate<Order>((shipment, order) =>
                {
                    var shipmentItem = shipment.ShipmentItems.FirstOrDefault(x => x.OrderItem.OrderId == order.Id);
                    if (shipmentItem == null)
                        return;
                    shipmentItem.OrderItem.Order = order;
                    order.OrderItems = order.OrderItems ?? new List<OrderItem>();
                    if(!order.OrderItems.Contains(shipmentItem.OrderItem))
                        order.OrderItems.Add(shipmentItem.OrderItem);

                    order.Shipments = order.Shipments ?? new List<Shipment>();
                    if (!order.Shipments.Contains(shipment))
                        order.Shipments.Add(shipment);
                })
                .Relate<User>((shipment, user) =>
                {
                    shipment.User = user;
                })
                .SelectNested()
                .FirstOrDefault();
        }

    }
}