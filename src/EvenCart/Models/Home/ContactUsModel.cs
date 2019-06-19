using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Home
{
    public class ContactUsModel : FoundationModel, IRequiresValidations<ContactUsModel>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public void SetupValidationRules(ModelValidator<ContactUsModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Email).EmailAddress().NotEmpty();
            v.RuleFor(x => x.Subject).Must((rootObject, x, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", 10);
                return x.Trim().Length > 10;
            }).WithMessage(LocalizationHelper.Localize("{{PropertyName}} should be at least {{MinLength}} characters long",
                ApplicationEngine.CurrentLanguageCultureCode));

            v.RuleFor(x => x.Description).Must((rootObject, x, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", 30);
                return x.Trim().Length > 30;
            }).WithMessage(LocalizationHelper.Localize("{{PropertyName}} should be at least {{MinLength}} characters long",
                ApplicationEngine.CurrentLanguageCultureCode));
        }
    }
}