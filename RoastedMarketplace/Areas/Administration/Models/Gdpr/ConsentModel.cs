using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Gdpr
{
    public class ConsentModel : FoundationEntityModel, IRequiresValidations<ConsentModel>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPluginSpecificConsent { get; set; }

        public string PluginSystemName { get; set; }

        public bool IsRequired { get; set; }

        public int DisplayOrder { get; set; }

        public string LanguageCultureCode { get; set; }

        public bool EnableLogging { get; set; }

        public bool Published { get; set; }

        public ConsentGroupModel ConsentGroup { get; set; }

        public bool OneTimeSelection { get; set; }

        public void SetupValidationRules(ModelValidator<ConsentModel> v)
        {
            v.RuleFor(x => x.Title).NotEmpty();
        }
    }
}