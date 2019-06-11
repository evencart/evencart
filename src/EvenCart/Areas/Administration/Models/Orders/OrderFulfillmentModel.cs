using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// The order fulfillment object
    /// </summary>
    public class OrderFulfillmentModel : FoundationModel
    {
        /// <summary>
        /// The id of the order item
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// The quantity of the ordered item
        /// </summary>
        public int Quantity { get; set; }
    }
}