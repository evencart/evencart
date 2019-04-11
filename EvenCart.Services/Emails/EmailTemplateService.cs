using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
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
            return GetProcessedContentTemplate(parentTemplate)
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