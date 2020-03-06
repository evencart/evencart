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
using EvenCart.Data.Entity.Promotions;

namespace EvenCart.Services.Extensions
{
    public static class DiscountCouponExtensions
    {
        public static decimal GetDiscountAmount(this DiscountCoupon discountCoupon, decimal amount, int times)
        {
            if (amount == 0)
                return 0;
            amount = times * amount;
            var maxAmount = discountCoupon.MaximumDiscountAmount;
            if (maxAmount == 0)
                maxAmount = decimal.MaxValue;

            var discountAmount = discountCoupon.CalculationType == CalculationType.FixedAmount
                ? discountCoupon.DiscountValue * times
                : (amount * discountCoupon.DiscountValue) / 100;

            if (discountAmount > maxAmount)
                discountAmount = maxAmount;
            return discountAmount;
        }

        public static List<int> RestrictionIds(this DiscountCoupon discountCoupon)
        {
            return discountCoupon.RestrictionValues?.Select(x => int.Parse(x.RestrictionIdentifier)).ToList() ?? new List<int>();
        }

        public static List<string> RestrictionValues(this DiscountCoupon discountCoupon)
        {
            return discountCoupon.RestrictionValues?.Select(x => x.RestrictionIdentifier).ToList() ?? new List<string>();
        }
    }
}