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