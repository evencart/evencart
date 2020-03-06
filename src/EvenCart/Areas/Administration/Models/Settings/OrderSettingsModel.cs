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

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class OrderSettingsModel : SettingsModel
    {
        public string OrderNumberTemplate { get; set; }

        public bool AllowReorder { get; set; }

        public bool AllowGuestCheckout { get; set; }

        public bool EnableWishlist { get; set; }

        public bool AllowReturns { get; set; }

        public int DefaultReturnsDays { get; set; }

        public bool AllowCancellation { get; set; }

        public IList<string> CancellationAllowedFor { get; set; }
    }
}