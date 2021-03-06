﻿#region License
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
using Genesis.Services;
using EvenCart.Data.Entity.Promotions;

namespace EvenCart.Services.Promotions
{
    public interface IDiscountCouponService : IGenesisEntityService<DiscountCoupon>
    {
        DiscountCoupon GetByCouponCode(string couponCode);

        IEnumerable<DiscountCoupon> SearchDiscountCoupons(string searchText, out int totalMatches, int page = 1, int count = 15);

        void SetRestrictionIdentifiers(int discountCouponId, IList<string> restrictionIdentifiers);
    }
}