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

namespace EvenCart.Data.Entity.Shop
{
    /// <summary>
    /// Represents a weight unit
    /// </summary>
    public enum WeightUnit
    {
        /// <summary>
        /// The gram unit
        /// </summary>
        [Description("gm(s)")]
        Gram = 1,
        /// <summary>
        /// The kilogram unit
        /// </summary>
        [Description("kg(s)")]
        Kilogram = 2,
        /// <summary>
        /// The ounce unit
        /// </summary>
        [Description("oz")]
        Ounce = 3,
        /// <summary>
        /// The pound unit
        /// </summary>
        [Description("lb(s)")]
        Pound = 4,
        /// <summary>
        /// The ton unit
        /// </summary>
        [Description("ton(s)")]
        Ton = 5
    }
}