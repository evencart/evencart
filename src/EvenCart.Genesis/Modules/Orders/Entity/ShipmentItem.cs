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

using Genesis.Data;

namespace EvenCart.Data.Entity.Purchases
{
    public class ShipmentItem : GenesisEntity
    {
        public int ShipmentId { get; set; }

        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        #region Virtual Properties
        public virtual OrderItem OrderItem { get; set; }

        public virtual Shipment Shipment { get; set; }
        #endregion

    }
}