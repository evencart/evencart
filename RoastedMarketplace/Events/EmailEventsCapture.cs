using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Factories.Orders;
using RoastedMarketplace.Factories.Shipments;
using RoastedMarketplace.Factories.Users;
using RoastedMarketplace.Services.Emails;
using RoastedMarketplace.Services.Extensions;

namespace RoastedMarketplace.Events
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
        public EmailEventsCapture(IEmailSender emailSender, UserSettings userSettings, EmailSenderSettings emailSenderSettings, IOrderModelFactory orderModelFactory, IUserModelFactory userModelFactory, IShipmentModelFactory shipmentModelFactory)
        {
            _emailSender = emailSender;
            _userSettings = userSettings;
            _emailSenderSettings = emailSenderSettings;
            _orderModelFactory = orderModelFactory;
            _userModelFactory = userModelFactory;
            _shipmentModelFactory = shipmentModelFactory;
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
            NamedEvent.PasswordResetRequested.ToString()
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