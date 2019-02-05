using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Emails
{
    public class EmailTemplateService : FoundationEntityService<EmailTemplate>, IEmailTemplateService
    {
        public string GetProcessedContentTemplate(EmailTemplate emailTemplate)
        {
            if (emailTemplate == null)
                return "";

            if (emailTemplate.ParentEmailTemplate == null)
                return emailTemplate.Template;
            return
                GetProcessedContentTemplate(emailTemplate.ParentEmailTemplate)
                    .Replace(EmailTokenNames.MessageContent, emailTemplate.Template);
        }
        
        public override EmailTemplate Get(int id)
        {
            return FirstOrDefault(x => x.Id == id);
        }

        public override EmailTemplate FirstOrDefault(Expression<Func<EmailTemplate, bool>> @where)
        {
            return Repository.Join<EmailAccount>("EmailAccountId", "Id")
                .Join<EmailTemplate>("ParentEmailTemplateId", "Id", typeof(EmailTemplate))
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