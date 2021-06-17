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
    /// Represents a return option 
    /// </summary>
    public enum ReturnOption
    {
        /// <summary>
        /// The customer should be refunded
        /// </summary>
        [Description("Refund the money to the customer")]
        Refund = 10,
        /// <summary>
        /// A return order should be created for fresh items
        /// </summary>
        [Description("Create a new order to return fresh items")]
        ReturnFresh = 20,
        /// <summary>
        /// A return order should be created for repaired items
        /// </summary>
        [Description("Create a new order to return repaird items")]
        ReturnRepaired = 30,
        /// <summary>
        /// Any other option
        /// </summary>
        [Description("Other")]
        Other = 40
    }
}