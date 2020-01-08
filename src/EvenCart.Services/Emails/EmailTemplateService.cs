using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Emails
{
    public class EmailTemplateService : FoundationEntityService<EmailTemplate>, IEmailTemplateService
    {
        public string GetProcessedContentTemplate(EmailTemplate emailTemplate)
        {
            if (emailTemplate == null)
                return "";

            if (emailTemplate.ParentEmailTemplateId == 0)
                return emailTemplate.Template;
            var parentTemplate = emailTemplate.ParentEmailTemplate ?? Get(emailTemplate.ParentEmailTemplateId);
            emailTemplate.ParentEmailTemplate = parentTemplate;
            return GetProcessedContentTemplate(parentTemplate)
                .Replace(EmailTokenNames.MessageContent, emailTemplate.Template);
        }

        public IList<string> GetTemplateTokens(string templateName)
        {
            switch (templateName)
            {
                case EmailTemplateNames.UserRegisteredMessage:
                case EmailTemplateNames.UserRegisteredMessageToAdmin:
                case EmailTemplateNames.UserActivatedMessage:
                case EmailTemplateNames.PasswordChangedMessage: 
                case EmailTemplateNames.UserDeactivatedMessage:
                case EmailTemplateNames.UserDeactivatedMessageToAdmin:
                case EmailTemplateNames.UserAccountDeletedMessage:
                case EmailTemplateNames.UserAccountDeletedMessageToAdmin:
                    return new List<string>()
                    {
                        "user",
                        "store"
                    };
                case EmailTemplateNames.UserActivationLinkMessage:
                    return new List<string>()
                    {
                        "user",
                        "store",
                        "activationLink"
                    };
                case EmailTemplateNames.PasswordRecoveryLinkMessage:
                    return new List<string>()
                    {
                        "user",
                        "store",
                        "passwordResetLink"
                    };
                case EmailTemplateNames.OrderPlacedMessage: 
                case EmailTemplateNames.OrderPlacedMessageToAdmin:
                case EmailTemplateNames.OrderPaidMessage: 
                case EmailTemplateNames.OrderPaidMessageToAdmin:
                    return new List<string>()
                    {
                        "user",
                        "store",
                        "order"
                    };
              
                case EmailTemplateNames.ShipmentShippedMessage:
                case EmailTemplateNames.ShipmentDeliveredMessage:
                case EmailTemplateNames.ShipmentDeliveredMessageToAdmin:
                    return new List<string>()
                    {
                        "user",
                        "orders",
                        "shipment",
                        "store"
                    };
                default: return new List<string>()
                {
                    "user",
                    "store"
                };
            }
        }

        public override EmailTemplate Get(int id)
        {
            return FirstOrDefault(x => x.Id == id);
        }

        public override EmailTemplate FirstOrDefault(Expression<Func<EmailTemplate, bool>> @where)
        {
            return Repository.Join<EmailAccount>("EmailAccountId", "Id")
                .Join<EmailTemplate>("ParentEmailTemplateId", "Id", typeof(EmailTemplate), JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<EmailTemplate, EmailAccount>())
                .Relate<EmailTemplate>((template, emailTemplate) =>
                {
                    if (template.ParentEmailTemplateId == emailTemplate.Id)
                        template.ParentEmailTemplate = emailTemplate;
                })
                .OrderBy(x => x.TemplateName)
                .Where(where)
                .SelectNested()
                .FirstOrDefault();
        }
    }
}