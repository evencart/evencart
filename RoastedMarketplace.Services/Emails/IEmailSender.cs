using System;
using RoastedMarketplace.Data.Entity.Emails;

namespace RoastedMarketplace.Services.Emails
{
    public interface IEmailSender
    {
        bool SendTestEmail(string email, EmailAccount emailAccount, out Exception ex);

        void SendEmail(string templateName, EmailMessage.UserInfo userInfo, object model, bool sendToUser = true, bool sendEmailToAdmin = false);

        void SendUserRegistered(EmailMessage.UserInfo userInfo, object model = null);

        void SendUserActivationLink(EmailMessage.UserInfo userInfo, object model = null);

        void SendUserActivated(EmailMessage.UserInfo userInfo, object model = null);

        void SendOrderPlaced(EmailMessage.UserInfo userInfo, object model = null);

        void SendOrderComplete(EmailMessage.UserInfo userInfo, object model = null);

        void SendShipmentShipped(EmailMessage.UserInfo userInfo, object model = null);

        void SendShipmentDelivered(EmailMessage.UserInfo userInfo, object model = null);
    }
}