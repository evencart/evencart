using System;
using System.Net;
using System.Net.Mail;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Exception;
using RoastedMarketplace.Core.Infrastructure.AppEngine;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Logger;
using RoastedMarketplace.Services.Security;
using RoastedMarketplace.Services.VerboseReporter;

namespace RoastedMarketplace.Services.Emails
{
    public class EmailService : FoundationEntityService<EmailMessage>, IEmailService
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly ICryptographyService _cryptographyService;
        private readonly ILogger _logger;

        public EmailService(IFoundationEntityRepository<EmailMessage> dataRepository, IEmailAccountService emailAccountService, ICryptographyService cryptographyService, ILogger logger) : base(dataRepository)
        {
            _emailAccountService = emailAccountService;
            _cryptographyService = cryptographyService;
            _logger = logger;
        }

        public bool SendEmail(EmailMessage emailMessage, bool verboseErrorOnFailure = false)
        {
            //we need an email account
            var emailAccount = emailMessage.EmailAccount ?? _emailAccountService.FirstOrDefault(x => x.IsDefault);
            if (emailAccount == null)
                return false; //can't send email without account

            var message = new MailMessage();
            //from, to, reply to
            message.From = new MailAddress(emailAccount.Email, emailAccount.FromName);

            if (emailMessage.Tos == null && emailMessage.Ccs == null && emailMessage.Bccs == null)
            {
                throw new RoastedMarketplaceException("At least one of Tos, CCs or BCCs must be specified to send email");
            }

            if (emailMessage.Tos != null)
                foreach (var userInfo in emailMessage.Tos)
                {
                    message.To.Add(new MailAddress(userInfo.Email, userInfo.Name));
                }

            if (emailMessage.ReplyTos != null)
                foreach (var userInfo in emailMessage.ReplyTos)
                {
                    message.ReplyToList.Add(new MailAddress(userInfo.Email, userInfo.Name));
                }

            if (emailMessage.Bccs != null)
                foreach (var userInfo in emailMessage.Bccs)
                {
                    message.Bcc.Add(new MailAddress(userInfo.Email, userInfo.Name));
                }

            if (emailMessage.Ccs != null)
                foreach (var userInfo in emailMessage.Ccs)
                {
                    message.Bcc.Add(new MailAddress(userInfo.Email, userInfo.Name));
                }

            //content
            message.Subject = emailMessage.Subject;
            message.Body = emailMessage.EmailBody;
            message.IsBodyHtml = emailMessage.IsEmailBodyHtml;

            //headers
            if (emailMessage.Headers != null)
                foreach (var header in emailMessage.Headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

            if (emailMessage.Attachments != null)
                foreach (var attachment in emailMessage.Attachments)
                    message.Attachments.Add(attachment);

            //send email

            var password = _cryptographyService.Decrypt(emailAccount.Password);
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.UseSsl;
                smtpClient.Credentials = emailAccount.UseDefaultCredentials ?
                    CredentialCache.DefaultNetworkCredentials :
                    new NetworkCredential(emailAccount.UserName, password);
                try
                {
                    smtpClient.Send(message);
                    //update the send status
                    emailMessage.IsSent = true;
                    Update(emailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    //log the error
                    _logger.Log<EmailService>(LogLevel.Error, $"Failed to send email to {emailMessage.TosSerialized}", ex);
                    if (verboseErrorOnFailure)
                    {
                        var verboseReporterService = RoastedMarketplaceEngine.ActiveEngine.Resolve<IVerboseReporterService>();
                        verboseReporterService.ReportError(ex.Message, "send_email");
                    }
                    return false;
                }
            }
        }

        public void Queue(EmailMessage emailMessage)
        {
            Insert(emailMessage);
        }
    }
}
