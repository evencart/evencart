using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Gdpr
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