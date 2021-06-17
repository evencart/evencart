﻿#region License
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
using Genesis.Data;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis.Modules.Users;

namespace EvenCart.Data.Entity.Reviews
{
    public class Review : GenesisEntity
    {
        public int Rating { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public bool VerifiedPurchase { get; set; }

        public int? OrderId { get; set; }

        public int ProductId { get; set; }

        public bool Published { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool Private { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}