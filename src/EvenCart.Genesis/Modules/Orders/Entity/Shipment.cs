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

using System.Collections.Generic;
using Genesis.Data;
using EvenCart.Data.Entity.Shop;
using Genesis.Modules.Users;

namespace EvenCart.Data.Entity.Purchases
{
    public class Shipment : GenesisEntity
    {
        public string TrackingNumber { get; set; }

        public string Remarks { get; set; }

        public string ShippingMethodName { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public int WarehouseId { get; set; }

        public string ShippingLabelUrl { get; set; }

        public string TrackingUrl { get; set; }

        #region Virtual Properties
        public virtual IList<ShipmentItem> ShipmentItems { get; set; }

        public virtual IList<ShipmentHistory> ShipmentStatusHistories { get; set; }

        public virtual User User { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        #endregion
    }
}