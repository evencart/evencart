using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Gdpr
{
    public class ConsentModel : FoundationEntityModel, IRequiresValidations<ConsentModel>
    {
        /// <summary>
        /// The title of the consent. It can include html tags. Ignored for POST requests.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the consent. It can include html tags. Ignored for POST requests.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Specifies if the consent is required. Default is false. Ignored for POST requests.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// The consent status of consent
        /// </summary>
        public ConsentStatus ConsentStatus { get; set; }

        /// <summary>
        /// If true, the consent is required to be provided during user registration. Default is false. Ignored for POST requests.
        /// </summary>
        public bool OneTimeSelection { get; set; }

        public void SetupValidationRules(ModelValidator<ConsentModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}