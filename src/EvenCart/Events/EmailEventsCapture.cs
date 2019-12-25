using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Services.Emails;
using EvenCart.Services.Extensions;
using EvenCart.Factories.Orders;
using EvenCart.Factories.Shipments;
using EvenCart.Factories.Users;
using EvenCart.Models.Orders;

namespace EvenCart.Events
{
    public class EmailEventsCapture : IEventCapture
    {
        private EmailModel R => new EmailModel();

        private readonly IEmailSender _emailSender;
        private readonly UserSettings _userSettings;
        private readonly EmailSenderSettings _emailSenderSettings;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IShipmentModelFactory _shipmentModelFactory;
        private readonly SecuritySettings _securitySettings;
        private readonly IReturnRequestModelFactory _returnRequestModelFactory;
        public EmailEventsCapture(IEmailSender emailSender, UserSettings userSettings, EmailSenderSettings emailSenderSettings, IOrderModelFactory orderModelFactory, IUserModelFactory userModelFactory, IShipmentModelFactory shipmentModelFactory, SecuritySettings securitySettings, IReturnRequestModelFactory returnRequestModelFactory)
        {
            _emailSender = emailSender;
            _userSettings = userSettings;
            _emailSenderSettings = emailSenderSettings;
            _orderModelFactory = orderModelFactory;
            _userModelFactory = userModelFactory;
            _shipmentModelFactory = shipmentModelFactory;
            _securitySettings = securitySettings;
            _returnRequestModelFactory = returnRequestModelFactory;
        }

        public void Capture(string eventName, object[] eventData = null)
        {
            if (eventData == null)
                return;
            switch (eventName)
            {
                case nameof(NamedEvent.UserRegistered):
                    if (_emailSenderSettings.UserRegisteredEmailEnabled ||
                        _emailSenderSettings.UserRegisteredEmailToAdminEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);
                        var model = R.With("user", userModel);

                        _emailSender.SendEmail(EmailTemplateNames.UserRegisteredMessage, userInfo, model.Result,
                            _emailSenderSettings.UserRegisteredEmailEnabled, _emailSenderSettings.UserRegisteredEmailToAdminEnabled);

                        if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.WithActivationEmail)
                        {
                            var activationLink = "";
                            model.With("activationLink", activationLink);
                            _emailSender.SendEmail(EmailTemplateNames.UserActivationLinkMessage, userInfo, model.Result);
                        }
                    }
                    break;
                case nameof(NamedEvent.UserActivated):
                    if (_emailSenderSettings.UserActivationEmailEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);
                        var model = R.With("user", userModel);
                        _emailSender.SendEmail(EmailTemplateNames.UserActivatedMessage, userInfo, model);
                    }
                    break;
                case nameof(NamedEvent.OrderPlaced):
                    if (_emailSenderSettings.OrderPlacedEmailEnabled || _emailSenderSettings.OrderPlacedEmailToAdminEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var order = (Order)eventData[1];
                        var userModel = _userModelFactory.Create(user);
                        var orderModel = _orderModelFactory.Create(order);
                        var model = R.With("user", userModel).With("order", orderModel).Result;

                        _emailSender.SendEmail(EmailTemplateNames.OrderPlacedMessage, userInfo, model,
                            _emailSenderSettings.OrderPlacedEmailEnabled,
                            _emailSenderSettings.OrderPlacedEmailToAdminEnabled);
                    }
                    break;
                case nameof(NamedEvent.OrderPaid):
                    if (_emailSenderSettings.OrderPaidEmailEnabled || _emailSenderSettings.OrderPaidEmailToAdminEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var order = (Order)eventData[1];
                        var userModel = _userModelFactory.Create(user);
                        var orderModel = _orderModelFactory.Create(order);
                        var model = R.With("user", userModel).With("order", orderModel).Result;

                        _emailSender.SendEmail(EmailTemplateNames.OrderPaidMessage, userInfo, model,
                            _emailSenderSettings.OrderPaidEmailEnabled,
                            _emailSenderSettings.OrderPaidEmailToAdminEnabled);
                    }
                    break;
                case nameof(NamedEvent.ShipmentShipped):
                    if (_emailSenderSettings.ShipmentShippedEmailEnabled)
                    {
                        var shipment = (Shipment)eventData[0];
                        var user = shipment.User;
                        var userModel = _userModelFactory.Create(user);
                        var userInfo = shipment.User.ToUserInfo();
                        var orders = shipment.ShipmentItems.Select(x => _orderModelFactory.Create(x.OrderItem.Order)).ToList();
                        var shipmentModel = _shipmentModelFactory.Create(shipment);

                        var model = R.With("user", userModel).With("orders", orders).With("shipment", shipmentModel)
                            .Result;
                        _emailSender.SendEmail(EmailTemplateNames.ShipmentShippedMessage, userInfo, model);
                    }
                    break;
                case nameof(NamedEvent.ShipmentDelivered):
                    if (_emailSenderSettings.ShipmentDeliveredEmailEnabled || _emailSenderSettings.ShipmentDeliveredEmailToAdminEnabled)
                    {
                        var shipment = (Shipment)eventData[0];
                        var user = shipment.User;
                        var userModel = _userModelFactory.Create(user);
                        var userInfo = shipment.User.ToUserInfo();
                        var orders = shipment.ShipmentItems.Select(x => _orderModelFactory.Create(x.OrderItem.Order)).ToList();
                        var shipmentModel = _shipmentModelFactory.Create(shipment);

                        var model = R.With("user", userModel).With("orders", orders).With("shipment", shipmentModel)
                            .Result;
                        _emailSender.SendEmail(EmailTemplateNames.ShipmentDeliveredMessage, userInfo, model,
                            _emailSenderSettings.ShipmentDeliveredEmailEnabled,
                            _emailSenderSettings.ShipmentDeliveredEmailToAdminEnabled);
                    }
                    break;
                case nameof(NamedEvent.ShipmentDeliveryFailed):
                    if (_emailSenderSettings.ShipmentDeliveryFailedEmailEnabled || _emailSenderSettings.ShipmentDeliveryFailedToAdminEmailEnabled)
                    {
                        var shipment = (Shipment)eventData[0];
                        var user = shipment.User;
                        var userModel = _userModelFactory.Create(user);
                        var userInfo = shipment.User.ToUserInfo();
                        var orders = shipment.ShipmentItems.Select(x => _orderModelFactory.Create(x.OrderItem.Order)).ToList();
                        var shipmentModel = _shipmentModelFactory.Create(shipment);

                        var model = R.With("user", userModel).With("orders", orders).With("shipment", shipmentModel)
                            .Result;
                        _emailSender.SendEmail(EmailTemplateNames.ShipmentDeliveredMessage, userInfo, model,
                            _emailSenderSettings.ShipmentDeliveryFailedEmailEnabled,
                            _emailSenderSettings.ShipmentDeliveryFailedToAdminEmailEnabled);
                    }
                    break;
                case nameof(NamedEvent.PasswordResetRequested):
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);
                        var resetLink = eventData[1];
                        var model = R.With("user", userModel).With("passwordResetLink", resetLink).Result;
                        _emailSender.SendEmail(EmailTemplateNames.PasswordRecoveryLinkMessage, userInfo, model);
                    }
                    break;
                case nameof(NamedEvent.PasswordReset):
                    if (_emailSenderSettings.PasswordChangedEmailEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);
                        var model = R.With("user", userModel).Result;
                        _emailSender.SendEmail(EmailTemplateNames.PasswordChangedMessage, userInfo, model);
                    }
                    break;
                case nameof(NamedEvent.InvitationRequested):
                    {
                        var email = eventData[0].ToString();
                        var userInfo = new EmailMessage.UserInfo("", email);
                        var model = R.With("email", email).Result;
                        _emailSender.SendEmail(EmailTemplateNames.InvitationRequestedMessage, userInfo, model,
                            _emailSenderSettings.InviteRequestCreatedEmailEnabled,
                            _emailSenderSettings.InviteRequestCreatedEmailToAdminEnabled);
                    }
                    break;
                case nameof(NamedEvent.Invitation):
                    {
                        var userCode = (UserCode)eventData[0];
                        var invitationLink = eventData[1].ToString();
                        var userInfo = new EmailMessage.UserInfo("", userCode.Email);
                        var hours = _securitySettings.InviteLinkExpirationHours;
                        var model = R.With("invitationLink", invitationLink).With("email", userCode.Email).With("expirationHours", hours).Result;
                        _emailSender.SendEmail(EmailTemplateNames.InvitationMessage, userInfo, model);
                    }
                    break;
                case nameof(NamedEvent.ReturnRequestCreated):
                    {
                        var user = (User)eventData[0];
                        var order = (Order)eventData[1];
                        var listItems = ((List<ReturnRequest>)eventData[2]).Select(x => _returnRequestModelFactory.Create(x)).ToList();
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);

                        var model = R.With("user", userModel).With("order", order).With("returnRequests", listItems).Result;
                        _emailSender.SendEmail(EmailTemplateNames.ReturnRequestCreatedMessage, userInfo, model,
                            _emailSenderSettings.ReturnRequestCreatedEmailEnabled,
                            _emailSenderSettings.ReturnRequestCreatedToAdminEmailEnabled);
                    }
                    break;
                case nameof(NamedEvent.VendorRegistered):
                case nameof(NamedEvent.VendorActivated):
                case nameof(NamedEvent.VendorRejected):
                    if (_emailSenderSettings.VendorRegisteredEmailEnabled ||
                        _emailSenderSettings.VendorRegisteredEmailToAdminEnabled ||
                        _emailSenderSettings.VendorActivatedEmailEnabled ||
                        _emailSenderSettings.VendorDeactivatedEmailEnabled ||
                        _emailSenderSettings.VendorRejectedEmailEnabled)
                    {
                        var user = (User)eventData[0];
                        var userInfo = user.ToUserInfo();
                        var userModel = _userModelFactory.Create(user);
                        var model = R.With("user", userModel);

                        var vendor = (Vendor) eventData[1];
                        model.With("vendor", vendor);

                        if (eventName == nameof(NamedEvent.VendorRegistered))
                            _emailSender.SendEmail(EmailTemplateNames.VendorRegisteredMessage, userInfo, model.Result,
                                _emailSenderSettings.VendorRegisteredEmailEnabled,
                                _emailSenderSettings.VendorRegisteredEmailToAdminEnabled);
                        else if (eventName == nameof(NamedEvent.VendorActivated))
                            _emailSender.SendEmail(EmailTemplateNames.VendorActivatedMessage, userInfo, model.Result,
                                _emailSenderSettings.VendorActivatedEmailEnabled);
                        else if (eventName == nameof(NamedEvent.VendorRejected))
                        {
                            var message = eventData[2]?.ToString();
                            model.With("rejectReason", message);
                            _emailSender.SendEmail(EmailTemplateNames.VendorRejectedMessage, userInfo, model.Result,
                                _emailSenderSettings.VendorRejectedEmailEnabled);
                        }
                        else if (eventName == nameof(NamedEvent.VendorDeactivated))
                        {
                            var message = eventData[2]?.ToString();
                            model.With("deactivationReason", message);
                            _emailSender.SendEmail(EmailTemplateNames.VendorDeactivatedMessage, userInfo, model.Result,
                                _emailSenderSettings.VendorDeactivatedEmailEnabled);
                        }
                    }
                    break;
                case nameof(NamedEvent.ContactUs):
                    {
                        var contactUsModel = eventData[0];
                        var model = R.With("contact", contactUsModel);
                        _emailSender.SendEmail(EmailTemplateNames.ContactUsMessageToAdmin, null, model, false, true);
                    }
                    break;
            }
        }

        public string[] EventNames { get; } =
        {
            NamedEvent.UserRegistered.ToString(),
            NamedEvent.OrderPaid.ToString(),
            NamedEvent.OrderPlaced.ToString(),
            NamedEvent.ShipmentShipped.ToString(),
            NamedEvent.ShipmentDelivered.ToString(),
            NamedEvent.PasswordReset.ToString(),
            NamedEvent.PasswordResetRequested.ToString(),
            NamedEvent.InvitationRequested.ToString(),
            NamedEvent.Invitation.ToString(),
            NamedEvent.ReturnRequestCreated.ToString(),
            NamedEvent.ContactUs.ToString()
        };


        #region helper classes

        private class EmailModel
        {
            internal Dictionary<string, object> Result { get; set; } = new Dictionary<string, object>();

            public EmailModel With(string key, object obj)
            {
                if (Result.ContainsKey(key))
                    Result[key] = obj;
                else
                {
                    Result.Add(key, obj);
                }

                return this;
            }
        }
        #endregion
    }
}