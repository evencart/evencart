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

using System;
using System.Collections.Generic;
using Genesis.Data;

namespace EvenCart.Data.Entity.Promotions
{
    public class DiscountCoupon : GenesisEntity
    {
        public string Name { get; set; }

        public bool HasCouponCode { get; set; }

        public string CouponCode { get; set; }

        public int NumberOfTimesPerUser { get; set; }

        public int TotalNumberOfTimes { get; set; }

        public decimal MaximumDiscountAmount { get; set; }

        public CalculationType CalculationType { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool ExcludeAlreadyDiscountedProducts { get; set; }

        public bool Enabled { get; set; }

        public RestrictionType RestrictionType { get; set; }

        public decimal MinimumOrderSubTotal { get; set; }

        #region Virtual Properties

        public virtual bool Expired => EndDate.HasValue && EndDate.Value <= DateTime.UtcNow;

        public virtual IList<RestrictionValue> RestrictionValues { get; set; }

        #endregion

    }
}