using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Emails
{
    public class EmailTemplateModel : FoundationEntityModel, IRequiresValidations<EmailTemplateModel>
    {
        public string TemplateName { get; set; }

        public string TemplateSystemName { get; set; }

        public string Template { get; set; }

        public bool IsMaster { get; set; }

        public int ParentEmailTemplateId { get; set; }

        public int EmailAccountId { get; set; }

        public string Subject { get; set; }

        public string AdministrationEmail { get; set; }

        public EmailTemplateModel ParentEmailTemplate { get; set; }

        public EmailAccountModel EmailAccount { get; set; }

        public void SetupValidationRules(ModelValidator<EmailTemplateModel> v)
        {
            v.RuleFor(x => x.TemplateName).NotEmpty();
            v.RuleFor(x => x.TemplateSystemName).NotEmpty();
            v.RuleFor(x => x.EmailAccountId).GreaterThan(0);
        }
    }
}