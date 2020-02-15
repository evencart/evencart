namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// Represents shipping info object
    /// </summary>
    public class ShipmentInfo
    {
        /// <summary>
        /// Tracking url of the shipment
        /// </summary>
        public string TrackingUrl { get; set; }

        /// <summary>
        /// Tracking number of the shipment
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Url of the shipping label
        /// </summary>
        public string ShippingLabelUrl { get; set; }

    }
}