using EvenCart.Core.Services;
using EvenCart.Data.Entity.Emails;

namespace EvenCart.Services.Emails
{
    public interface IEmailTemplateService : IFoundationEntityService<EmailTemplate>
    {
        string GetProcessedContentTemplate(EmailTemplate emailTemplate);
    }
}