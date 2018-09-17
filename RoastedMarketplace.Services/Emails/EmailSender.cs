using System;
using System.Collections.Generic;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Tickets;
using RoastedMarketplace.Data.Entity.Users;
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
        #endregion

        public EmailSender(IEmailService emailService, ITokenProcessor tokenProcessor, IEmailTemplateService emailTemplateService, IUserService userService, EmailSenderSettings emailSenderSettings)
        {
            _emailService = emailService;
            _tokenProcessor = tokenProcessor;
            _emailTemplateService = emailTemplateService;
            _userService = userService;
            _emailSenderSettings = emailSenderSettings;
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
            var template = _emailTemplateService.(x => x.TemplateSystemName == templateName);
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
                Subject = subjectString,
                OriginalEmailTemplate = template,
                Tos = new List<EmailMessage.UserInfo>()
            };

            return emailMessage;
        }

        private void QueueEmailMessage(string templateName, User user, params object[] entities)
        {
            var message = LoadAndProcessTemplate(templateName, user, entities);
            message.Tos.Add(new EmailMessage.UserInfo(user.Name, user.Email));
            _emailService.Queue(message);
        }

        private void QueueEmailMessageToAdmin(string templateName, params object[] entities)
        {
            var message = LoadAndProcessTemplate(templateName, entities);
            message.Tos.Add(new EmailMessage.UserInfo("Administrator", message.OriginalEmailTemplate.AdministrationEmail));
            _emailService.Queue(message);
        }

        public bool SendTestEmail(string email, EmailAccount emailAccount)
        {
            var subject = "MobSocial Test Email";
            var message = "Thank you for using mobSocial. This is a sample email to test if emails are functioning.";
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
            return _emailService.SendEmail(emailMessage, true);
        }

        public void SendUserRegisteredMessage(User user)
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

        public void SendUserActivationLinkMessage(User user, string activationUrl)
        {
            QueueEmailMessage(EmailTemplateNames.UserActivationLinkMessage, user);
        }

        public void SendUserActivatedMessage(User user)
        {
            if (_emailSenderSettings.UserActivationEmailEnabled)
                QueueEmailMessage(EmailTemplateNames.UserActivatedMessage, user);
        }

        public void SendTicketCreatedMessage(User user, Ticket ticket, bool agentCreatedTicket = false)
        {
            if (agentCreatedTicket)
            {
                if (_emailSenderSettings.TicketCreatedByAgentEmailEnabled)
                {
                    QueueEmailMessage(EmailTemplateNames.TicketCreatedMessage, user, ticket);
                }
                if (_emailSenderSettings.TicketCreatedByAgentEmailToAdminEnabled)
                {
                    QueueEmailMessageToAdmin(EmailTemplateNames.TicketCreatedMessageToAdmin, user, ticket);
                }
            }
            else
            {
                if (_emailSenderSettings.TicketCreatedByUserEmailEnabled)
                {
                    QueueEmailMessage(EmailTemplateNames.TicketCreatedMessage, user, ticket);
                }
                if (_emailSenderSettings.TicketCreatedByUserEmailToAdminEnabled)
                {
                    QueueEmailMessageToAdmin(EmailTemplateNames.TicketCreatedMessageToAdmin, user, ticket);
                }
            }
        }

        public void SendTicketUpdatedMessage(User user, Ticket ticket)
        {
            if (_emailSenderSettings.TicketUpdatedEmailEnabled)
            {
                QueueEmailMessage(EmailTemplateNames.TicketUpdatedMessage, user, ticket);
            }
            if (_emailSenderSettings.TicketUpdatedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.TicketUpdatedMessageToAdmin, user, ticket);
            }
        }

        public void SendTicketClosedMessage(User user, Ticket ticket)
        {
            if (_emailSenderSettings.TicketClosedEmailEnabled)
            {
                QueueEmailMessage(EmailTemplateNames.TicketClosedMessage, user, ticket);
            }
            if (_emailSenderSettings.TicketClosedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.TicketClosedMessageToAdmin, user, ticket);
            }
        }

        public void SendTicketDeletedMessage(User user, Ticket ticket)
        {
            if (_emailSenderSettings.TicketDeletedEmailEnabled)
            {
                QueueEmailMessage(EmailTemplateNames.TicketDeletedMessage, user, ticket);
            }
            if (_emailSenderSettings.TicketDeletedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.TicketDeletedMessageToAdmin, user, ticket);
            }
        }

        public void SendSlaViolationReminderMessage(User user, SlaPolicy slaPolicy, Ticket ticket)
        {
            QueueEmailMessage(EmailTemplateNames.SlaViolationReminderMessage, user, slaPolicy, ticket);
        }

        public void SendTicketResolvedMessage(User user, Ticket ticket)
        {
            if (_emailSenderSettings.TicketResolvedEmailEnabled)
            {
                QueueEmailMessage(EmailTemplateNames.TicketResolvedMessage, user, ticket);
            }
            if (_emailSenderSettings.TicketResolvedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.TicketResolvedMessageToAdmin, user, ticket);
            }
        }

        public void SendSlaViolatedMessage(User user, SlaPolicy slaPolicy, Ticket ticket)
        {
            QueueEmailMessage(EmailTemplateNames.SlaViolatedMessage, user, slaPolicy, ticket);
            if (_emailSenderSettings.SlaViolatedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.SlaViolatedMessageToAdmin, slaPolicy, ticket);
            }
        }

        public void SendSlaModifiedMessage(User user, SlaPolicy slaPolicy)
        {
            if (_emailSenderSettings.SlaModifiedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.SlaModifiedMessageToAdmin, user, slaPolicy);
            }
        }

        public void SendSlaDeletedMessage(User user, SlaPolicy slaPolicy)
        {
            if (_emailSenderSettings.SlaModifiedEmailToAdminEnabled)
            {
                QueueEmailMessageToAdmin(EmailTemplateNames.SlaModifiedMessageToAdmin, user, slaPolicy);
            }
        }
    }
}