using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    /// <summary>
    /// The inventory information object
    /// </summary>
    public class WarehouseProductInventoryModel : FoundationEntityModel
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
        public class InventoryModel : FoundationEntityModel
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