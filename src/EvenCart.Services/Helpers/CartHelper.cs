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