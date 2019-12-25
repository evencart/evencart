using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class EmailSenderSettings : ISettingGroup
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

        public bool OrderPaidEmailEnabled { get; set; }

        public bool OrderPaidEmailToAdminEnabled { get; set; }

        public bool ShipmentShippedEmailEnabled { get; set; }

        public bool ShipmentDeliveredEmailEnabled { get; set; }

        public bool ShipmentDeliveredEmailToAdminEnabled { get; set; }

        public bool ShipmentDeliveryFailedEmailEnabled { get; set; }

        public bool ShipmentDeliveryFailedToAdminEmailEnabled { get; set; }

        public bool ProductOutOfStockToAdminEnabled { get; set; }

        public bool InviteRequestCreatedEmailEnabled { get; set; }

        public bool InviteRequestCreatedEmailToAdminEnabled { get; set; }

        public bool ReturnRequestCreatedEmailEnabled { get; set; }

        public bool ReturnRequestCreatedToAdminEmailEnabled { get; set; }

        public bool VendorRegisteredEmailEnabled { get; set; }

        public bool VendorRegisteredEmailToAdminEnabled { get; set; }

        public bool VendorActivatedEmailEnabled { get; set; }

        public bool VendorRejectedEmailEnabled { get; set; }

        public bool VendorDeactivatedEmailEnabled { get; set; }
    }
}