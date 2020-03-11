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

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class EmailSettingsModel : SettingsModel
    {
        public int DefaultEmailAccountId { get; set; }

        public bool UserRegisteredEmailEnabled { get; set; }

        public bool UserRegisteredEmailToAdminEnabled { get; set; }

        public bool UserActivationEmailEnabled { get; set; }

        public bool PasswordChangedEmailEnabled { get; set; }

        public bool UserDeactivationEmailEnabled { get; set; }

        public bool UserDeactivationEmailToAdminEnabled { get; set; }

        public bool UserDeletedEmailEnabled { get; set; }

        public bool UserDeletedEmailToAdminEnabled { get; set; }

        public bool OrderPlacedEmailEnabled { get; set; }

        public bool OrderPlacedEmailToAdminEnabled { get; set; }

        public bool OrdrePaidEmailEnabled { get; set; }

        public bool OrderPaidEmailToAdminEnabled { get; set; }

        public bool ShipmentShippedEmailEnabled { get; set; }

        public bool ShipmentDeliveredEmailEnabled { get; set; }

        public bool ShipmentDeliveredEmailToAdminEnabled { get; set; }

        public bool ProductOutOfStockToAdminEnabled { get; set; }
    }
}