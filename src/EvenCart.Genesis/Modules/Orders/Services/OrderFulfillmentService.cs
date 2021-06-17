﻿#region License
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
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using Genesis.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis.Extensions;
using Genesis.Modules.Addresses;
using Genesis.Modules.MediaServices;

namespace EvenCart.Services.Orders
{
    public class OrderFulfillmentService : GenesisEntityService<OrderFulfillment>, IOrderFulfillmentService
    {
        public override IEnumerable<OrderFulfillment> Get(Expression<Func<OrderFulfillment, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<OrderItem>("OrderItemId", "Id", joinType: JoinType.LeftOuter)
                .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductMedia>("Id", "ProductId", joinType: JoinType.LeftOuter)
                .Join<Media>("MediaId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariant>("ProductVariantId", "Id", typeof(OrderItem), joinType: JoinType.LeftOuter)
                .Join<WarehouseInventory>("WarehouseId", "WarehouseId", SourceColumn.Parent, joinType: JoinType.LeftOuter)
                .Join<Warehouse>("WarehouseId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<OrderFulfillment, OrderItem>())
                .Relate(RelationTypes.OneToOne<OrderFulfillment, Warehouse>())
                .Relate<Address>((fulfillment, address) => { fulfillment.Warehouse.Address = address; })
                .Relate<Product>((fulfillment, product) => { fulfillment.OrderItem.Product = product; })
                .Relate<ProductVariant>((fulfillment, variant) =>
                {
                    if (fulfillment.OrderItem.ProductVariantId > 0)
                        fulfillment.OrderItem.ProductVariant = variant;
                })
                .Relate(RelationTypes.OneToOne<OrderFulfillment, WarehouseInventory>((fulfillment, inventory) =>
                {
                    IList<WarehouseInventory> inventories;
                    if (inventory.ProductVariantId > 0)
                    {
                        if (inventory.ProductVariant == null)
                            return;
                        fulfillment.OrderItem.ProductVariant.Inventories =
                            fulfillment.OrderItem.ProductVariant.Inventories ?? new List<WarehouseInventory>();
                        inventories = fulfillment.OrderItem.ProductVariant.Inventories;
                       
                    }
                    else
                    {
                        fulfillment.OrderItem.Product.Inventories =
                            fulfillment.OrderItem.Product.Inventories ?? new List<WarehouseInventory>();
                        inventories = fulfillment.OrderItem.Product.Inventories;
                    }

                    if (!inventories.Contains(inventory))
                        inventories.Add(inventory);
                }))
                .Relate<Media>((fulfillment, media) =>
                {
                    var product = fulfillment.OrderItem.Product;
                    product.MediaItems =
                        product.MediaItems ?? new List<Media>();
                    if (!product.MediaItems.Contains(media))
                        product.MediaItems.Add(media);
                })
                .Where(where)
                .SelectNested(page, count);
        }
    }
}