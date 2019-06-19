using System;
using System.Collections.Generic;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Emails;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.ViewEngines;
using EvenCart.Infrastructure.ViewEngines.Expanders;

namespace EvenCart.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        #region fields
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUserService _userService;
        private readonly EmailSenderSettings _emailSenderSettings;
        private readonly ILogger _logger;
        private readonly IViewAccountant _viewAccountant;
        #endregion

        public EmailSender(IEmailService emailService, IEmailTemplateService emailTemplateService, IUserService userService, EmailSenderSettings emailSenderSettings, ILogger logger, IViewAccountant viewAccountant)
        {
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
            _emailSenderSettings = emailSenderSettings;
            _logger = logger;
            _viewAccountant = viewAccountant;
        }

        /// <summary>
        /// Loads a named email template from database and replaces tokens with passed entities, and returns a new email message object with template values
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private EmailMessage LoadAndProcessTemplate(string templateName, object model = null)
        {
            //first load the template from database
            var template = _emailTemplateService.FirstOrDefault(x => x.TemplateSystemName == templateName);
            if (template == null)
                return null;

            var content = _emailTemplateService.GetProcessedContentTemplate(template);
            //render the content
            //expand the routes first
            content = Expander.ExpandRoutes(content, model);
            var processedTemplateString = _viewAccountant.RenderView(templateName, content, model);
            var subjectString = _viewAccountant.RenderView($"{templateName}:Subject", template.Subject, model);
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

        private void QueueEmailMessage(string templateName, EmailMessage.UserInfo user, object model = null)
        {
            var message = LoadAndProcessTemplate(templateName, model);
            if (message == null)
            {
                _logger.LogError<EmailTemplate>(null, $"Failure sending email. Unable to load template '{templateName}'");
                return;
            }
            message.Tos.Add(new EmailMessage.UserInfo(user.Name, user.Email));
            _emailService.Queue(message);
        }

        private void QueueEmailMessageToAdmin(string templateName, object model = null, string email = null)
        {
            var message = LoadAndProcessTemplate(templateName, model);
            if (message == null)
            {
                _logger.LogError<EmailTemplate>(null, $"Failure sending email. Unable to load template '{templateName}'");
                return;
            }

            email = email ?? message.OriginalEmailTemplate.AdministrationEmail;
            message.Tos.Add(new EmailMessage.UserInfo("Administrator", email));
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

        public void SendEmail(string templateName, EmailMessage.UserInfo userInfo, object model, bool sendToUser = true, bool sendEmailToAdmin = false)
        {
            if (sendToUser)
                QueueEmailMessage(templateName, userInfo, model);
            if (sendEmailToAdmin)
                QueueEmailMessageToAdmin(templateName + EmailTemplateNames.AdminSuffix, model);
        }
    }
}