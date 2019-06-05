using System.Linq;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Extensions
{
    public static class ProductExtensions
    {
        public static bool IsAvailableInStock(this Product product)
        {
            return product.CanOrderWhenOutOfStock ||
                   (product.Inventories?.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity) ?? true);
        }

        public static bool IsAvailableInStock(this ProductVariant variant, Product product)
        {
            return variant.Inventories?.Any(x => x.AvailableQuantity > product.MinimumPurchaseQuantity) ?? true;
        }
    }
}