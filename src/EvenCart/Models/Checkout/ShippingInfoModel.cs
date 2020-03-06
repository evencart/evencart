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

using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Checkout
{
    /// <summary>
    /// Represents a shipping information model
    /// </summary>
    public class ShippingInfoModel : FoundationModel
    {
        /// <summary>
        /// The <see cref="ShippingMethodModel">shippingMethod</see> to be used. Ignore if not applicable.
        /// </summary>
        public ShippingMethodModel ShippingMethod { get; set; }

        /// <summary>
        /// The list of <see cref="ShippingOptionModel">shipping option</see> to be used. Ignore if not applicable.
        /// </summary>
        public IList<ShippingOptionModel> ShippingOption { get; set; }
    }
}