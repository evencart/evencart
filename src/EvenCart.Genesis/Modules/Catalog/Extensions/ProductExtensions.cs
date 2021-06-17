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
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis;
using Genesis.Modules.Meta;

namespace EvenCart.Data.Extensions
{
    public static class ProductExtensions
    {
        /// <summary>
        /// Checks if a product is available in public store
        /// </summary>
        public static bool IsPublic(this Product product, int storeId)
        {
            var catalogIds = product?.Catalogs?.Select(x => x.Id).ToList() ?? new List<int>();
            if (!catalogIds.Any())
                return false; //no catalog means product is not available in any catalog
            var entityStoresService = D.Resolve<IEntityStoreService>();
            var catalogStores = entityStoresService.Get(x => x.EntityName == nameof(Catalog) && x.StoreId == storeId && catalogIds.Contains(x.EntityId));
            if (!catalogStores.Any())
                return false;
            return product != null && product.Published && !product.Deleted;
        }

        public static bool IsAvailableInStock(this Product product)
        {
            return product.CanOrderWhenOutOfStock ||
                   (product.Inventories?.Any(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity) ?? false);
        }

        public static bool IsAvailableInStock(this Product product, int quantity, int warehouseId)
        {
            return product.Inventories?.Where(x => x.WarehouseId == warehouseId).Any(x => x.AvailableQuantity >= quantity) ?? false;
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product)
        {
            return variant.CanOrderWhenOutOfStock ||
                   (variant.Inventories?.Any(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity) ?? false);
        }

        public static bool IsAvailableInStock(this ProductVariant variant, int quantity)
        {
            return variant.Inventories?.Any(x => x.AvailableQuantity >= quantity) ?? false;
        }

        public static bool IsAvailableInStock(this Product product, int quantity)
        {
            return product.Inventories?.Any(x => x.AvailableQuantity >= quantity) ?? false;
        }

        public static bool IsAvailableInStock(this ProductVariant variant, int quantity, int warehouseId)
        {
            return variant.Inventories?.Where(x => x.WarehouseId == warehouseId).Any(x => x.AvailableQuantity >= quantity) ?? false;
        }

        public static bool IsAvailableInStock(this Product product, out Warehouse warehouse)
        {
            warehouse = null;
            warehouse = product.Inventories?.Where(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity)
                .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            
            if (warehouse == null && (!product.TrackInventory || product.CanOrderWhenOutOfStock))
            {
                warehouse = product.Inventories?.OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            }
            return !product.TrackInventory || IsAvailableInStock(product);
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product, out Warehouse warehouse)
        {
            warehouse = null;
            warehouse = variant.Inventories?.Where(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity)
              .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            if (warehouse == null && (!variant.TrackInventory || variant.CanOrderWhenOutOfStock))
            {
                warehouse = variant.Inventories?.OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            }
            return !variant.TrackInventory || IsAvailableInStock(variant, product);
        }

        public static WarehouseInventory GetWarehouseInventory(this OrderItem orderItem, int warehouseId)
        {
            return orderItem.ProductVariantId > 0
                ? orderItem.ProductVariant.Inventories.FirstOrDefault(x => x.WarehouseId == warehouseId)
                : orderItem.Product.Inventories.FirstOrDefault(x => x.WarehouseId == warehouseId);
        }

        public static bool IsAvailableInStock(this OrderItem orderItem, int quantity)
        {
            return orderItem.ProductVariantId > 0
                ? orderItem.ProductVariant.IsAvailableInStock(quantity)
                : orderItem.Product.IsAvailableInStock(quantity);
        }

        public static string GetVariantName(this ProductVariant variant)
        {
            var attributes =
                variant.ProductVariantAttributes.Select(x =>
                    $"{x.ProductAttribute.Label}:{x.ProductAttributeValue.Label}");
            return string.Join(" ", attributes);
        }
    }
}