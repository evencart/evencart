using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Emails;

namespace RoastedMarketplace.Services.Emails
{
    public interface IEmailTemplateService : IFoundationEntityService<EmailTemplate>
    {
        string GetProcessedContentTemplate(EmailTemplate emailTemplate);
    }
}