using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Emails;

namespace RoastedMarketplace.Services.Emails
{
    public interface IEmailService : IFoundationEntityService<EmailMessage>
    {
        /// <summary>
        /// Sends an email with settings specified in the email info object and returns true if sending succeeds
        /// </summary>
        bool SendEmail(EmailMessage emailMessage, bool verboseErrorOnFailure = false);

        void Queue(EmailMessage emailMessage);
    }
}