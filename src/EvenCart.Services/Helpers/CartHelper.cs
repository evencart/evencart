using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;

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
    }
}