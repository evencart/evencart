using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Countries
{
    public class StateOrProvinceModel : FoundationEntityModel, IRequiresValidations<StateOrProvinceModel>
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public bool Published { get; set; }

        public bool ShippingEnabled { get; set; }

        public int DisplayOrder { get; set; }

        public void SetupValidationRules(ModelValidator<StateOrProvinceModel> v)
        {
            v.RuleFor(x => x.CountryId).GreaterThan(0);
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}