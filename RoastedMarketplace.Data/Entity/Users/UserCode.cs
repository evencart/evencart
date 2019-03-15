using System;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class UserCode : FoundationEntity
    {
        public int UserId { get; set; }

        public string Code { get; set; }

        public DateTime DateCreated { get; set; }

        public UserCodeType CodeType { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}