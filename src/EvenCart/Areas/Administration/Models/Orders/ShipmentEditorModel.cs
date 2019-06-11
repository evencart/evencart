using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents shipments grouped by warehouses
    /// </summary>
    public class ShipmentEditorModel : FoundationModel
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