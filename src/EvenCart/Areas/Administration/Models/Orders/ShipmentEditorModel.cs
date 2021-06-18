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
using EvenCart.Areas.Administration.Models.Warehouse;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents shipments grouped by warehouses
    /// </summary>
    public class ShipmentEditorModel : GenesisModel
    {
        /// <summary>
        /// The <see cref="WarehouseMiniModel">warehouse</see> object
        /// </summary>
        public WarehouseMiniModel Warehouse { get; set; }

        /// <summary>
        /// A list <see cref="ShipmentModel">shipment</see> objects for the warehouse
        /// </summary>
        public IList<ShipmentModel> Shipments { get; set; }
    }
}