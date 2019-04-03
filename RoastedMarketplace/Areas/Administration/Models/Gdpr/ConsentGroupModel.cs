using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Gdpr
{
    public class ConsentGroupModel : FoundationEntityModel, IRequiresValidations<ConsentGroupModel>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsSystemGroup { get; set; }

        public void SetupValidationRules(ModelValidator<ConsentGroupModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}