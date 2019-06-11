using System.ComponentModel;

namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// The status of the shiopment
    /// </summary>
    public enum ShipmentStatus
    {
        /// <summary>
        /// The shipment is being prepared
        /// </summary>
        [Description("Preparing")]
        Preparing = 0,
        /// <summary>
        /// The shipment has been packaged
        /// </summary>
        [Description("Packaged")]
        Packaged = 10,
        /// <summary>
        /// The shipment has been dispatched and is in the transit.
        /// </summary>
        [Description("In transit")]
        InTransit = 20,
        /// <summary>
        /// The shipment is out for delivery
        /// </summary>
        [Description("Out for delivery")]
        OutForDelivery = 30,
        /// <summary>
        /// The delivery has failed
        /// </summary>
        [Description("Delivery failed")]
        DeliveryFailed = 40,
        /// <summary>
        /// The shipment has been delivered
        /// </summary>
        [Description("Delivered")]
        Delivered = 50,
        /// <summary>
        /// The shipment was returned
        /// </summary>
        [Description("Returned")]
        Returned = 60
    }
}