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

namespace EvenCart.Data.Entity.Cultures
{
    public enum Rounding
    {
        [Description("Default")]
        Default,

        [Description("Till x.00 - (2.73 rounds to 3.00 and 1.10 rounds to 1.00)")]
        RoundDot00,

        [Description("Till x.99 - (2.73 rounds to 2.99 and 1.10 rounds to 0.99)")]
        RoundDot99,

        [Description("Till x.99/x4.99 - (2.73 rounds to 2.99 and 1.40 rounds to 1.49)")]
        RoundDot99Or49,

        [Description("Till x.50 - (2.73 rounds to 2.50 and 1.19 rounds to 1.00)")]
        RoundDot50Or00,

        [Description("Till x.x5 - (2.73 rounds to 2.75 and 1.19 rounds to 1.25)")]
        RoundDotX5,

        [Description("Till x.x0 - (2.73 rounds to 2.70 and 1.19 rounds to 1.20)")]
        RoundDotX0
    }
}