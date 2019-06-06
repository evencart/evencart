using System.Diagnostics.SymbolStore;
using System.Linq;
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

        public static bool IsAvailableInStock(this ProductVariant variant, Product product)
        {
            return variant.Inventories?.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity) ?? false;
        }

        public static bool IsAvailableInStock(this Product product, out Warehouse warehouse)
        {
            warehouse = product.Inventories?.Where(x => x.AvailableQuantity > product.MinimumPurchaseQuantity)
                .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;

            return IsAvailableInStock(product);
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product, out Warehouse warehouse)
        {
            warehouse = variant.Inventories?.Where(x => x.AvailableQuantity > product.MinimumPurchaseQuantity)
                .OrderBy(x => x.Warehouse.DisplayOrder).FirstOrDefault()?.Warehouse;
            return IsAvailableInStock(variant, product);
        }
    }
}