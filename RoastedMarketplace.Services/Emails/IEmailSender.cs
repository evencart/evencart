using System;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Emails
{
    public interface IEmailSender
    {
        bool SendTestEmail(string email, EmailAccount emailAccount, out Exception ex);

        void SendUserRegistered(User user);

        void SendUserActivationLink(User user, string activationUrl);

        void SendUserActivated(User user);

        void SendOrderPlaced(User user, Order order);

        void SendOrderComplete(User user, Order order);

        void SendShipmentShipped(User user, Order order, Shipment shipment);

        void SendShipmentDelivered(User user, Order order, Shipment shipment);
    }
}