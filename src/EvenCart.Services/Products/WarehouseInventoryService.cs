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
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class WarehouseInventoryService : FoundationEntityService<WarehouseInventory>, IWarehouseInventoryService
    {
        public IEnumerable<WarehouseInventory> GetByProduct(int productId, int? warehouseId = null)
        {
            return GetByProducts(new List<int>() {productId}, warehouseId);
        }

        public IEnumerable<WarehouseInventory> GetByProducts(IList<int> productIds, int? warehouseId = null)
        {
            Expression<Func<WarehouseInventory, bool>> warehouseWhere = x => true;
            if (warehouseId.HasValue)
            {
                warehouseWhere = x => x.WarehouseId == warehouseId;
            }
            return Repository
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<WarehouseInventory, Warehouse>())
                .Relate<Address>((inventory, address) => { inventory.Warehouse.Address = address; })
                .Where(x => productIds.Contains(x.ProductId))
                .Where(warehouseWhere)
                .SelectNested();
        }

        public IEnumerable<WarehouseInventory> GetByProductVariants(IList<int> productVariantIds, int? warehouseId = null)
        {
            Expression<Func<WarehouseInventory, bool>> warehouseWhere = x => true;
            if (warehouseId.HasValue)
            {
                warehouseWhere = x => x.WarehouseId == warehouseId;
            }
            return Repository
                .Join<Warehouse>("WarehouseId", "Id", joinType: JoinType.LeftOuter)
                .Join<Address>("AddressId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<WarehouseInventory, Warehouse>())
                .Relate<Address>((inventory, address) => { inventory.Warehouse.Address = address; })
                .Where(x => x.ProductVariantId != null && productVariantIds.Contains((int) x.ProductVariantId))
                .Where(warehouseWhere)
                .SelectNested();
        }

        /*.Join<ProductVariant>("VariantId", "Id", joinType: JoinType.LeftOuter)
                .Join<ProductVariantAttribute>("Id", "ProductVariantId", joinType: JoinType.LeftOuter)
                .Join<ProductAttribute>("ProductAttributeId", "Id", typeof(ProductVariantAttribute), JoinType.LeftOuter)
                .Join<ProductAttributeValue>("ProductAttributeValueId", "Id", typeof(ProductVariantAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttribute>("AvailableAttributeId", "Id", typeof(ProductAttribute),
                    joinType: JoinType.LeftOuter)
                .Join<AvailableAttributeValue>("AvailableAttributeValueId", "Id", typeof(ProductAttributeValue),
                    joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<WarehouseInventory, ProductVariant>())
                .Relate<ProductVariantAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    if (variant != null)
                    {
                        attribute.ProductVariant = variant;
                        variant.ProductVariantAttributes =
                            variant.ProductVariantAttributes ?? new List<ProductVariantAttribute>();
                        if (!variant.ProductVariantAttributes.Contains(attribute))
                            variant.ProductVariantAttributes.Add(attribute);
                    }
                })
                .Relate<ProductAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute = attribute;
                        }
                    }
                })
                .Relate<ProductAttributeValue>((inventory, attributeValue) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue = attributeValue;
                        }
                    }
                })
                .Relate<AvailableAttribute>((inventory, attribute) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttribute.AvailableAttributeId == attribute.Id)
                        {
                            variantAttribute.ProductAttribute.AvailableAttribute = attribute;
                            if (variantAttribute.ProductAttribute.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttribute.Label = attribute.Name;
                        }
                    }
                })
                .Relate<AvailableAttributeValue>((inventory, attributeValue) =>
                {
                    var variant = inventory.ProductVariant;
                    foreach (var variantAttribute in variant.ProductVariantAttributes)
                    {
                        if (variantAttribute.ProductAttributeValue.AvailableAttributeValueId == attributeValue.Id)
                        {
                            variantAttribute.ProductAttributeValue.AvailableAttributeValue = attributeValue;
                            if (variantAttribute.ProductAttributeValue.Label.IsNullEmptyOrWhiteSpace())
                                variantAttribute.ProductAttributeValue.Label = attributeValue.Value;
                        }
                    }
                })*/
    }
}