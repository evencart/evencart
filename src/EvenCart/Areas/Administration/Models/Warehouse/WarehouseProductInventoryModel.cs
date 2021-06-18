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
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    /// <summary>
    /// The inventory information object
    /// </summary>
    public class WarehouseProductInventoryModel : GenesisEntityModel
    {
        /// <summary>
        /// A collection of <see cref="InventoryModel">variant inventory</see> objects
        /// </summary>
        public IList<InventoryModel> Variants { get; set; }

        /// <summary>
        /// A collection of <see cref="InventoryModel">product inventory</see> objects
        /// </summary>
        public IList<InventoryModel> Products { get; set; }

        /// <summary>
        /// The inventory object
        /// </summary>
        public class InventoryModel : GenesisEntityModel
        {
            /// <summary>
            /// The warehouse id
            /// </summary>
            public int WarehouseId { get; set; }

            /// <summary>
            /// The warehouse name
            /// </summary>
            public string WarehouseName { get; set; }

            /// <summary>
            /// The name to identify the inventory item
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The quantity that is available to customers for new purchases
            /// </summary>
            public int AvailableQuantity { get; set; }

            /// <summary>
            /// The quantity that is available in the warehouse but not available for new purchases due to existing orders
            /// </summary>
            public int ReservedQuantity { get; set; }

            /// <summary>
            /// The total quantity present in the warehouse
            /// </summary>
            public int TotalQuantity { get; set; }
        }
    }
}