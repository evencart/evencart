using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// The order fulfillment group object
    /// </summary>
    public class OrderFulfillmentListModel : FoundationModel
    {
        /// <summary>
        /// The <see cref="WarehouseModel">warehouse</see> in this fulfillment
        /// </summary>
        public WarehouseModel Warehouse { get; set; }

        /// <summary>
        /// A list of <see cref="OrderItemModel">order item</see> objects in this fulfillment
        /// </summary>
        public IList<OrderItemModel> OrderItems { get; set; }

    }
}