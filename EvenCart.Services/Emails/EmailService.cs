using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using DotEntity.Enumerations;
using EvenCart.Core.Exception;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Logger;
using EvenCart.Services.Security;

namespace EvenCart.Services.Emails
{
    public class EmailService : FoundationEntityService<EmailMessage>, IEmailService
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly ICryptographyService _cryptographyService;
        private readonly ILogger _logger;
        private readonly EmailSenderSettings _emailSenderSettings;
        public EmailService(IEmailAccountService emailAccountService, ICryptographyService cryptographyService, ILogger logger, EmailSenderSettings emailSenderSettings)
        {
            _emailAccountService = emailAccountService;
            _cryptographyService = cryptographyService;
            _logger = logger;
            _emailSenderSettings = emailSenderSettings;
        }

        public bool SendEmail(EmailMessage emailMessage, out Exception exception)
        {
            exception = null;
            //we need an email account
            var emailAccount = emailMessage.EmailAccount ??
                               _emailAccountService.Get(_emailSenderSettings.DefaultEmailAccountId) ??
                               _emailAccountService.FirstOrDefault(x => true);
            if (emailAccount == null)
            {
                _logger.Log<EmailService>(LogLevel.Error, $"Failed to send email. No default email account could be loaded.");
                return false; //can't send email without account
            }

            var message = new MailMessage();
            //from, to, reply to
            message.From = new MailAddress(emailAccount.Email, emailAccount.FromName);

            if (emailMessage.Tos == null && emailMessage.Ccs == null && emailMessage.Bccs == null)
            {
                throw new EvenCartException("At least one of Tos, CCs or BCCs must be specified to send email");
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
                    if (emailMessage.Id > 0)
                        Update(emailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    //log the error
                    _logger.Log<EmailService>(LogLevel.Error, $"Failed to send email to {emailMessage.TosSerialized}", ex);
                    return false;
                }
            }
        }

        public bool SendEmail(EmailMessage emailMessage)
        {
            return SendEmail(emailMessage, out Exception _);
        }

        public void Queue(EmailMessage emailMessage)
        {
            Insert(emailMessage);
        }

        public override IEnumerable<EmailMessage> Get(Expression<Func<EmailMessage, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<EmailAccount>("EmailAccountId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<EmailMessage, EmailAccount>())
                .Where(where)
                .OrderBy(x => x.Id)

                .SelectNested(page, count);
        }
    }
}
