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