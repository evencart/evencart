#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Shipping
{
    public class ShipmentService : FoundationEntityService<Shipment>, IShipmentService
    {
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
                .Join<Warehouse>("WarehouseId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
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
                    if (shipmentItem != null)
                        shipmentItem.OrderItem.Product = product;

                })
                .Relate(RelationTypes.OneToOne<Shipment, Warehouse>())
                .Relate<Address>((shipment, address) => { shipment.Warehouse.Address = address; })
                .Where(whereOrderIdMatches)
                .SelectNested()
                .ToList();
        }

        public override Shipment Get(int id)
        {
            Expression<Func<SeoMeta, bool>> where = meta => meta.EntityName == nameof(Product);
            return Repository.Where(x => x.Id == id)
                .Join<ShipmentItem>("Id", "ShipmentId", joinType: JoinType.LeftOuter)
                .Join<OrderItem>("OrderItemId", "Id", joinType: JoinType.LeftOuter)
                .Join<Order>("OrderId", "Id", joinType: JoinType.LeftOuter)
                .Join<ShipmentHistory>("Id", "ShipmentId", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<User>("UserId", "Id", typeof(Order), JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", typeof(OrderItem), JoinType.LeftOuter)
                .Join<SeoMeta>("Id", "EntityId", joinType: JoinType.LeftOuter)
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
                .Relate(RelationTypes.OneToOne<Shipment, Warehouse>())
                .Relate<Address>((shipment, address) => { shipment.Warehouse.Address = address; })
                .Relate<Product>((shipment, product) =>
                {
                    foreach (var shipmentItem in shipment.ShipmentItems)
                    {
                        if (shipmentItem.OrderItem.ProductId == product.Id)
                            shipmentItem.OrderItem.Product = product;
                    }
                })
                .Relate<SeoMeta>((shipment, meta) =>
                {
                    foreach (var shipmentItem in shipment.ShipmentItems)
                    {
                        if (shipmentItem.OrderItem.ProductId == meta.EntityId)
                            shipmentItem.OrderItem.Product.SeoMeta = meta;
                    }
                })
                .SelectNested()
                .FirstOrDefault();
        }

    }
}