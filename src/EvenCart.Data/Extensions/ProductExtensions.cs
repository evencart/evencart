using System.Diagnostics.SymbolStore;
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Extensions
{
    public static class ProductExtensions
    {
        public static bool IsAvailableInStock(this Product product)
        {
            return product.CanOrderWhenOutOfStock ||
                   (product.Inventories?.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity) ?? false);
        }

        public static bool IsAvailableInStock(this Product product, int quantity, int warehouseId)
        {
            return product.Inventories?.Where(x => x.WarehouseId == warehouseId).Any(x => x.AvailableQuantity >= quantity) ?? false;
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product)
        {
            return variant.Inventories?.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity) ?? false;
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
            warehouse = product.Inventories?.Where(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity)
                .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;

            return IsAvailableInStock(product);
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product, out Warehouse warehouse)
        {
            warehouse = variant.Inventories?.Where(x => x.AvailableQuantity >= product.MinimumPurchaseQuantity)
                .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            return IsAvailableInStock(variant, product);
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
    }
}