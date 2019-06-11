namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// Represents an individual shipping option if a shipment handler
    /// </summary>
    public class ShippingOption
    {
        /// <summary>
        /// The unique identifier for the option
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
        /// <summary>
        /// The guaranteed number of days for delivery
        /// </summary>
        public int GuaranteedDaysToDelivery { get; set; }
        /// <summary>
        /// Any additional information about shipping option
        /// </summary>
        public string Remarks { get; set; }
    }
}