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