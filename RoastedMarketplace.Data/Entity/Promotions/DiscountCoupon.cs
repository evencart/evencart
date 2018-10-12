using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Promotions
{
    public class DiscountCoupon : FoundationEntity
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

        #region Virtual Properties

        public virtual bool Expired => EndDate.HasValue && EndDate.Value <= DateTime.UtcNow;

        public virtual IList<RestrictionValue> RestrictionValues { get; set; }

        #endregion

    }
}