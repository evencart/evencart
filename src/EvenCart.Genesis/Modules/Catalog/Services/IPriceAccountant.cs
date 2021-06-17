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
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Promotions;
using Genesis.Modules.Addresses;
using Genesis.Modules.Localization;
using Genesis.Modules.Users;

namespace EvenCart.Services.Products
{
    public interface IPriceAccountant
    {
        DiscountApplicationStatus ApplyDiscountCoupon(string couponCode, Cart cart);

        DiscountApplicationStatus ApplyDiscountCoupon(DiscountCoupon coupon, Cart cart);

        bool CanApplyDiscount(DiscountCoupon coupon, int userId, out DiscountApplicationStatus status);

        bool CanApplyDiscount(string couponCode, int userId, out DiscountApplicationStatus status);

        bool CanApplyDiscount(int couponCodeId, int userId, out DiscountApplicationStatus status);

        void ClearCouponCode(Cart cart);

        void RefreshCartParameters(Cart cart);

        decimal GetAutoDiscountedPriceForUser(Product product, ProductVariant variant, User user, int quantity, ref IList<DiscountCoupon> discountCoupons, out decimal discount);

        void GetProductPriceDetails(Product product, Address address, decimal? basePrice, out decimal price, out decimal tax, out decimal taxRate, out string taxName);

        decimal ConvertCurrency(decimal input, Currency targetCurrency, Rounding? roundingType = null);
    }
}