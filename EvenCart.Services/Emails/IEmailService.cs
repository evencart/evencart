using System;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Emails;

namespace EvenCart.Services.Emails
{
    public interface IEmailService : IFoundationEntityService<EmailMessage>
    {
        /// <summary>
        /// Sends an email with settings specified in the email info object and returns true if sending succeeds
        /// </summary>
        bool SendEmail(EmailMessage emailMessage, out Exception ex);

        /// <summary>
        /// Sends an email with settings specified in the email info object and returns true if sending succeeds
        /// </summary>
        bool SendEmail(EmailMessage emailMessage);

        void Queue(EmailMessage emailMessage);
    }
}