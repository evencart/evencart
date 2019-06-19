using System;
using EvenCart.Data.Entity.Emails;

namespace EvenCart.Services.Emails
{
    public interface IEmailSender
    {
        bool SendTestEmail(string email, EmailAccount emailAccount, out Exception ex);

        void SendEmail(string templateName, EmailMessage.UserInfo userInfo, object model, bool sendToUser = true, bool sendEmailToAdmin = false);       
    }
}