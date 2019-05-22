using System;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class UserCode : FoundationEntity
    {
        public int UserId { get; set; }

        public string Code { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserCodeType CodeType { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}