using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Services.Extensions
{
    public static class DiscountCouponExtensions
    {
        public static decimal GetDiscountAmount(this DiscountCoupon discountCoupon, decimal amount)
        {
            if (amount == 0)
                return 0;

            var maxAmount = discountCoupon.MaximumDiscountAmount;
            if (maxAmount == 0)
                maxAmount = decimal.MaxValue;

            var discountAmount = discountCoupon.CalculationType == CalculationType.FixedAmount
                ? discountCoupon.DiscountValue
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