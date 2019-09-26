using System;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Subscriptions
{
    public class Subscription : FoundationEntity
    {
        public string Email { get; set; }

        public int? UserId { get; set; }

        public string SubscriptionGuid { get; set; }

        public string Data { get; set; }

        public DateTime CreatedOn { get; set; }

        #region Virtual Properties

        public virtual string SubscriptionName { get; set; }

        #endregion
    }
}