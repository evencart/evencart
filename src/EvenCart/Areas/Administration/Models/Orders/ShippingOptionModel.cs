using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents an individual shipping option of a shipment handler
    /// </summary>
    public class ShippingOptionModel : FoundationModel
    {
        /// <summary>
        /// The id of the option
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The rate of this shipping option
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// The name of this shipping option
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the option
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The delivery time of this option
        /// </summary>
        public string DeliveryTime { get; set; }
    }
}