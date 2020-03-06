#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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
        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }
    }
}