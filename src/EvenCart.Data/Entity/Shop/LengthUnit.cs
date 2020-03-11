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
    /// Represents the length unit
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>
        /// The centimeter unit
        /// </summary>
        [Description("cm(s)")]
        Centimeter = 1,
        /// <summary>
        /// The meter unit
        /// </summary>
        [Description("meter(s)")]
        Meter = 2,
        /// <summary>
        /// The feet unit
        /// </summary>
        [Description("feet")]
        Feet = 3,
        /// <summary>
        /// The inch unit
        /// </summary>
        [Description("inch(es)")]
        Inch = 4,
        /// <summary>
        /// The yard unit
        /// </summary>
        [Description("yard(s)")]
        Yard = 5
    }
}