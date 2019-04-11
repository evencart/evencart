using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Emails
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