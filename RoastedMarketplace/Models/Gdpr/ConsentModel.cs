using FluentValidation;
using RoastedMarketplace.Data.Entity.Gdpr;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Models.Gdpr
{
    public class ConsentModel : FoundationEntityModel, IRequiresValidations<ConsentModel>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public ConsentStatus ConsentStatus { get; set; }

        public bool OneTimeSelection { get; set; }

        public void SetupValidationRules(ModelValidator<ConsentModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}