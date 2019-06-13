using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    /// <summary>
    /// Represents a single return item
    /// </summary>
    public class ReturnItemModel : FoundationModel
    {
        /// <summary>
        /// The order item corresponding to the return
        /// </summary>
        public OrderItemModel OrderItem { get; set; }

        /// <summary>
        /// The minimum quantity that should be returned
        /// </summary>
        public int MinimumQuantityToReturn { get; set; }
    }
}