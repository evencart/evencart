using System;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Purchases
{
    public class ReturnRequest : FoundationEntity
    {
        public int OrderId { get; set; }

        public int OrderItemId { get; set; }

        public int UserId { get; set; }

        public string ReturnReason { get; set; }

        public string ReturnAction { get; set; }

        public int Quantity { get; set; }

        public string CustomerComments { get; set; }

        public string StaffComments { get; set; }

        public string Remarks { get; set; }

        public ReturnRequestStatus ReturnRequestStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public ReturnOption ReturnOption { get; set; }

        public int? ReturnOrderId { get; set; }

        #region Virtual Properties
        public virtual OrderItem OrderItem { get; set; }

        public virtual Order Order { get; set; }

        public virtual Order ReturnOrder { get; set; }

        public virtual User User { get; set; }
        #endregion
    }
}