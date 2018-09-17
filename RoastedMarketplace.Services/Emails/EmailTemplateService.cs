using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Emails;

namespace RoastedMarketplace.Services.Emails
{
    public class EmailTemplateService : RoastedMarketplaceEntityService<EmailTemplate>, IEmailTemplateService
    {
        public EmailTemplateService(IFoundationEntityRepository<EmailTemplate> dataRepository) : base(dataRepository)
        {

        }

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
    }
}