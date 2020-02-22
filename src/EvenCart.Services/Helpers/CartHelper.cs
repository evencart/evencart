using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;

namespace EvenCart.Services.Helpers
{
    public static class CartHelper
    {
        /// <summary>
        /// Refreshes cart items to update quantity and prices
        /// </summary>
        /// <param name="cart"></param>
        public static void RefreshCart(Cart cart)
        {
            var priceAccountant = DependencyResolver.Resolve<IPriceAccountant>();
            priceAccountant.RefreshCartParameters(cart);
        }

        public static bool IsShippingRequired(Cart cart)
        {
            return cart.CartItems.Any(x => x.Product.IsShippable);
        }

        public static bool IsPaymentRequired(Cart cart)
        {
            return cart.FinalAmount > 0;
        }

        public static bool HasConflictingProducts(Cart cart)
        {
            return cart.CartItems.Count > 1 &&
                   cart.CartItems.Select(x => x.Product.ProductSaleType).Distinct().Count() > 1;
        }

        public static bool IsSubscriptionCart(Cart cart)
        {
            return cart.CartItems.All(x => x.Product.ProductSaleType != ProductSaleType.OneTime);
        }
    }
}