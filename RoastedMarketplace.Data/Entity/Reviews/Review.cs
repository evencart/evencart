using System;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Data.Entity.Reviews
{
    public class Review : FoundationEntity
    {
        public int Rating { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public bool VerifiedPurchase { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public bool Published { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool Private { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }

        public virtual Order Order { get; set; }

        #endregion
    }
}