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

using EvenCart.Core.Services;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Promotions;

namespace EvenCart.Services.Purchases
{
    public interface ICartService : IFoundationEntityService<Cart>
    {
        Cart GetCart(int userId);

        Cart GetWishlist(int userId);

        void AddToCart(int userId, CartItem item, Transaction transaction = null);

        void UpdateCart(int userId, CartItem item, Transaction transaction = null);

        void AddToWishlist(int userId, CartItem item, Transaction transaction = null);

        void RemoveFromCart(int cartItemId, Transaction transaction = null);

        void ClearCart(int userId);

        void ClearWishlist(int userId);

        void SetPaymentMethodOnCart(int userId, string paymentMethodName, string paymentMethodData, Cart cart = null);

        void SetShippingMethodOnCart(int userId, string shippingMethodName, Cart cart = null);

        DiscountApplicationStatus SetDiscountCoupon(int userId, string discountCoupon, Cart cart = null);

        void ClearDiscountCoupon(int userId, Cart cart = null);

        void SetAddresses(int userId, int billingAddressId, int shippingAddressId);
    }
}