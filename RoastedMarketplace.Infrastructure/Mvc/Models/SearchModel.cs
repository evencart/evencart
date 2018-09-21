using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public abstract class SearchModel : FoundationModel, IRequiresValidations<SearchModel>
    {
        public string Search { get; set; }

        public int Page { get; set; } = 1;

        public int Count { get; set; } = 15;

        public void SetupValidationRules(ModelValidator<SearchModel> v)
        {
            v.RuleFor(x => x.Page).GreaterThan(0);
            v.RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}