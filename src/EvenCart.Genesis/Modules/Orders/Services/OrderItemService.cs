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

using System.Collections.Generic;
using System.Linq;
using DotEntity;
using DotEntity.Enumerations;
using Genesis.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis.Extensions;

namespace EvenCart.Services.Orders
{
    public class OrderItemService : GenesisEntityService<OrderItem>, IOrderItemService
    {
        public override OrderItem Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<Order>("OrderId", "Id")
                .Relate(RelationTypes.OneToOne<OrderItem, Order>())
                .SelectNested()
                .FirstOrDefault();
        }

        public IEnumerable<OrderItem> GetWithProducts(IList<int> orderItemIds)
        {
            return Repository.Where(x => orderItemIds.Contains(x.Id))
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<WarehouseInventory>("Id", "ProductId", joinType: JoinType.LeftOuter)
                //.Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                //.Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariant>("ProductVariantId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<WarehouseInventory>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<Order>("OrderId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                //.Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                //.Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<OrderItem, Product>())
                .Relate(RelationTypes.OneToOne<OrderItem, ProductVariant>())
                .Relate(RelationTypes.OneToOne<OrderItem, Order>((item, order) =>
                    {
                        order.OrderItems = order.OrderItems ?? new List<OrderItem>();
                        if (!order.OrderItems.Contains(item))
                            order.OrderItems.Add(item);
                    }))
                .Relate<WarehouseInventory>((item, inventory) =>
                {
                    if (item.ProductVariantId > 0)
                    {
                        item.ProductVariant.Inventories =
                            item.ProductVariant.Inventories ?? new List<WarehouseInventory>();
                        if (!item.ProductVariant.Inventories.Contains(inventory))
                            item.ProductVariant.Inventories.Add(inventory);
                    }
                    else
                    {
                        item.Product.Inventories =
                            item.Product.Inventories ?? new List<WarehouseInventory>();
                        if (!item.Product.Inventories.Contains(inventory))
                            item.Product.Inventories.Add(inventory);
                    }
                })
                /*.Relate<Warehouse>((item, warehouse) =>
                {
                    var inventories = item.ProductVariantId > 0
                        ? item.ProductVariant.Inventories
                        : item.Product.Inventories;
                    if (inventories != null)
                    {
                        foreach (var inventory in inventories.Where(x => x.WarehouseId == warehouse.Id))
                            inventory.Warehouse = warehouse;
                    }
                })
                .Relate<Address>((item, address) =>
                {
                    var inventories = item.ProductVariantId > 0
                        ? item.ProductVariant.Inventories
                        : item.Product.Inventories;
                    if (inventories != null)
                    {
                        foreach (var inventory in inventories.Where(x => x.Warehouse.AddressId == address.Id))
                            inventory.Warehouse.Address = address;
                    }
                })*/
                .SelectNested();
        }
    }
}