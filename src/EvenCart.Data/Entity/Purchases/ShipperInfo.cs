namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// Represents the information of shipper
    /// </summary>
    public class ShipperInfo
    {
        /// <summary>
        /// The country code of the shipper
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The zip code of shipper
        /// </summary>
        public string ZipCode { get; set; }
    }
}