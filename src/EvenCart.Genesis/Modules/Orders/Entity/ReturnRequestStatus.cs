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
    /// The status of a refund request
    /// </summary>
    public enum ReturnRequestStatus
    {
        /// <summary>
        /// The return is pending
        /// </summary>
        Pending = 0,
        /// <summary>
        /// The return has been authorized
        /// </summary>
        Authorized = 10,
        /// <summary>
        /// The return process has been initiated
        /// </summary>
        Initiated = 20,
        /// <summary>
        /// The items have been scheduled for return pickup
        /// </summary>
        PickupScheduled = 30,
        /// <summary>
        /// The items have been picked up and are on way back
        /// </summary>
        InTransit = 40,
        /// <summary>
        /// The items have been received at the warehouse
        /// </summary>
        ReturnReceived = 50,
        /// <summary>
        /// The return is complete
        /// </summary>
        Complete = 60,
        /// <summary>
        /// The return has been cancelled
        /// </summary>
        Cancelled = 70,
        /// <summary>
        /// The return has been rejected
        /// </summary>
        Rejected = 80,
        /// <summary>
        /// The return item has been repaired
        /// </summary>
        Repaired = 90
    }
}