using System;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Data.Entity.Gdpr
{
    public class ConsentLog : FoundationEntity
    {
        public int ConsentId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public ActivityType ActivityType { get; set; }

        public string EncryptedUserInfo { get; set; }

        #region Virtual Properties

        public virtual User User { get; set; }

        public virtual Consent Consent { get; set; }

        #endregion
    }
}