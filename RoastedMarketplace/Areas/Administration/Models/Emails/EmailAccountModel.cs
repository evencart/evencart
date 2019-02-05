using FluentValidation;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Emails
{
    public class EmailAccountModel : FoundationEntityModel, IRequiresValidations<EmailAccountModel>
    {
        public string Email { get; set; }

        public string FromName { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsDefault { get; set; }

        public string TestEmail { get; set; }

        public void SetupValidationRules(ModelValidator<EmailAccountModel> v)
        {
            v.RuleFor(x => x.Email).EmailAddress();
            v.RuleFor(x => x.TestEmail).Must(s => s.IsNullEmptyOrWhiteSpace() || s.IsValidEmail());
            v.RuleFor(x => x.Password).NotEmpty().When(x => x.Id == 0);
        }
    }
}