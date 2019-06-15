using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Purchases
{
    public class OrderFulfillmentService : FoundationEntityService<OrderFulfillment>, IOrderFulfillmentService
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