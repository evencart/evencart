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