using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Services.Helpers
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
    }
}