using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Gdpr
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