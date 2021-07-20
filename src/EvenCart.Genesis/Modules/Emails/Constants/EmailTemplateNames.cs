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

namespace EvenCart.Genesis.Modules.Emails.Constants
{
    public class EmailTemplateNames
    {
        public const string AdminSuffix = ".Admin";

        public const string Master = "Master";

        public const string UserRegisteredMessage = "User.Registered";

        public const string UserRegisteredMessageToAdmin = UserRegisteredMessage + AdminSuffix;

        public const string UserActivatedMessage = "User.Activated";

        public const string UserActivationLinkMessage = "User.ActivationLink";

        public const string PasswordRecoveryLinkMessage = "Common.PasswordRecovery";

        public const string PasswordChangedMessage = "Common.PasswordChanged";

        public const string UserDeactivatedMessage = "User.Deactivated";

        public const string UserDeactivatedMessageToAdmin = UserDeactivatedMessage + AdminSuffix;

        public const string UserAccountDeletedMessage = "User.AccountDeleted";

        public const string UserAccountDeletedMessageToAdmin = UserAccountDeletedMessage + AdminSuffix;

        public const string OrderPlacedMessage = "Order.Placed";

        public const string OrderPlacedMessageToAdmin = OrderPlacedMessage + AdminSuffix;

        public const string OrderPaidMessage = "Order.Paid";

        public const string OrderPaidMessageToAdmin = OrderPaidMessage + AdminSuffix;

        public const string OrderAdvisorAssignedToCustomerMessage = "Order.AdvisorAssignedToCustomer";

        public const string OrderAdvisorAssignedToAdvisorMessage = "Order.AdvisorAssignedToAdvisor";

        public const string ShipmentShippedMessage = "Shipment.Shipped";

        public const string ShipmentDeliveredMessage = "Shipment.Delivered";

        public const string ShipmentDeliveredMessageToAdmin = ShipmentDeliveredMessage + AdminSuffix;

        public const string ShipmentDeliveryFailedMessage = "Shipment.Failed";

        public const string ShipmentDeliveryFailedMessageToAdmin = ShipmentDeliveryFailedMessage + AdminSuffix;

        public const string InvitationRequestedMessage = "Invitation.Requested";

        public const string InvitationRequestedMessageToAdmin = InvitationRequestedMessage + AdminSuffix;

        public const string InvitationMessage = "Invitation";

        public const string ReturnRequestCreatedMessage = "ReturnRequest.Created";

        public const string ReturnRequestCreatedMessageToAdmin = ReturnRequestCreatedMessage + AdminSuffix;

        public const string ContactUsMessageToAdmin = "ContactUs" + AdminSuffix;

        public const string VendorRegisteredMessage = "Vendor.Registered";

        public const string VendorRegisteredMessageToAdmin = VendorRegisteredMessage + AdminSuffix;

        public const string VendorActivatedMessage = "Vendor.Activated";

        public const string VendorRejectedMessage = "Vendor.Rejected";

        public const string VendorDeactivatedMessage = "Vendor.Deactivated";

        public const string CustomOrderMessage = "CustomOrder.Created";
    }
}