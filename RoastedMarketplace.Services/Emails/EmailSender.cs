using System;
using System.Collections.Generic;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Logger;
using RoastedMarketplace.Services.Tokens;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Emails
{
    public class EmailSender : IEmailSender
    {
        #region fields
        private readonly IEmailService _emailService;
        private readonly ITokenProcessor _tokenProcessor;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserService _userService;
        private readonly EmailSenderSettings _emailSenderSettings;
        private readonly ILogger _logger;
        #endregion

        public EmailSender(IEmailService emailService, ITokenProcessor tokenProcessor, IEmailTemplateService emailTemplateService, IUserService userService, EmailSenderSettings emailSenderSettings, ILogger logger)
        {
            _emailService = emailService;
            _tokenProcessor = tokenProcessor;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
            _emailSenderSettings = emailSenderSettings;
            _logger = logger;
        }

        /// <summary>
        /// Loads a named email template from database and replaces tokens with passed entities, and returns a new email message object with template values
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        private EmailMessage LoadAndProcessTemplate(string templateName, params object[] entities)
        {
            //first load the template from database
            var template = _emailTemplateService.FirstOrDefault(x => x.TemplateSystemName == templateName);
            if (template == null)
                return null;

            var processedTemplateString = _tokenProcessor.ProcessAllTokens(template.Template, entities);
            var subjectString = _tokenProcessor.ProcessAllTokens(template.Subject, entities);
            var emailAccount = template.EmailAccount;
            //create a new email message
            var emailMessage = new EmailMessage() {
                IsEmailBodyHtml = true,
                EmailBody = processedTemplateString,
                EmailAccount = emailAccount,
                EmailAccountId = emailAccount?.Id ?? 0,
                Subject = subjectString,
                OriginalEmailTemplate = template,
                SendingDate = DateTime.UtcNow,
                Tos = new List<EmailMessage.UserInfo>()
            };

            return emailMessage;
        }

        private void QueueEmailMessage(string templateName, User user, params object[] entities)
        {
            var message = LoadAndProcessTemplate(templateName, user, entities);
            if (message == null)
            {
                _logger.LogError<EmailTemplate>(null, user, $"Failure sending email. Unable to load template '{templateName}'");
                return;
            }
            message.Tos.Add(new EmailMessage.UserInfo(user.Name, user.Email));
            _emailService.Queue(message);
        }

        private void QueueEmailMessageToAdmin(string templateName, params object[] entities)
        {
            var message = LoadAndProcessTemplate(templateName, entities);
            if (message == null)
            {
                _logger.LogError<EmailTemplate>(null, null, $"Failure sending email. Unable to load template '{templateName}'");
                return;
            }
            message.Tos.Add(new EmailMessage.UserInfo("Administrator", message.OriginalEmailTemplate.AdministrationEmail));
            _emailService.Queue(message);
        }

        public bool SendTestEmail(string email, EmailAccount emailAccount, out Exception ex)
        {
            var subject = "Test Email";
            var message = "This is a sample email to test if emails are functioning.";
            //create a new email message
            var emailMessage = new EmailMessage() {
                IsEmailBodyHtml = true,
                EmailBody = message,
                EmailAccount = emailAccount,
                Subject = subject,
                Tos = new List<EmailMessage.UserInfo>()
                {
                    new EmailMessage.UserInfo("WebAdmin", email)
                }
            };
            return _emailService.SendEmail(emailMessage, out ex);
        }

        public void SendUserRegistered(User user)
        {
            if (_emailSenderSettings.UserRegisteredEmailEnabled)
            {
                QueueEmailMessage(EmailTemplateNames.UserRegisteredMessage, user);
            }
            if (_emailSenderSettings.UserRegisteredEmailToAdminEnabled) //send to admin if needed
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.UserRegisteredMessageToAdmin, user);
            }
        }

        public void SendUserActivationLink(User user, string activationUrl)
        {
            QueueEmailMessage(EmailTemplateNames.UserActivationLinkMessage, user);
        }

        public void SendUserActivated(User user)
        {
            if (_emailSenderSettings.UserActivationEmailEnabled)
                QueueEmailMessage(EmailTemplateNames.UserActivatedMessage, user);
        }

        public void SendOrderPlaced(User user, Order order)
        {
            
        }

        public void SendOrderComplete(User user, Order order)
        {
            
        }

        public void SendShipmentShipped(User user, Order order, Shipment shipment)
        {
            
        }

        public void SendShipmentDelivered(User user, Order order, Shipment shipment)
        {
            
        }
    }
}