using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    /// <summary>
    /// A return request object for single order item
    /// </summary>
    public class ReturnRequestModel : FoundationModel
    {
        /// <summary>
        /// The id of the order item
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// The quantity of the item to return
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The reason id for return request
        /// </summary>
        public int ReturnReasonId { get; set; }

        /// <summary>
        /// The action id for return request
        /// </summary>
        public int ReturnActionId { get; set; }

        /// <summary>
        /// The comments as provided by customer
        /// </summary>
        public string CustomerComments { get; set; }
    }
}