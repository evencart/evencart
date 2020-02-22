using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents order fulfillment details of an order
    /// </summary>
    public class OrderFulfillmentEditorModel : FoundationModel
    {
        /// <summary>
        /// The <see cref="WarehouseModel">warehouse</see> of the fulfillment
        /// </summary>
        public WarehouseModel Warehouse { get; set; }

        /// <summary>
        /// A collection of <see cref="OrderItemFulfillmentModel">order item</see> object
        /// </summary>
        public IList<OrderItemFulfillmentModel> OrderItems { get; set; }
        /// <summary>
        /// The single order item fulfillment object
        /// </summary>
        public class OrderItemFulfillmentModel : FoundationModel
        {
            /// <summary>
            /// A single <see cref="OrderItemModel">order item</see> object
            /// </summary>
            public OrderItemModel OrderItem { get; set; }
            /// <summary>
            /// The currency quantity of the item in the warehouse
            /// </summary>
            public int CurrentQuantity { get; set; }

            /// <summary>
            /// The available quantity of the item in the warehouse
            /// </summary>
            public int AvailableQuantity { get; set; }

            /// <summary>
            /// Specifies if the order fulfillment is locked for editing
            /// </summary>
            public bool Locked { get; set; }
        }
    }
}