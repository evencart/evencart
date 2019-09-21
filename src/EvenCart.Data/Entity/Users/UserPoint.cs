using System;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Users
{
    public class UserPoint : FoundationEntity
    {
        public int UserId { get; set; }

        public int Points { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Reason { get; set; }

        public int ActivatorUserId { get; set; }

        #region Virtual Properties
        public virtual User ActivatorUser { get; set; }
        #endregion
    }
}