using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Navigation
{
    public class MenuModel : FoundationEntityModel, IRequiresValidations<MenuModel>
    {
        public string Name { get; set; }

        public void SetupValidationRules(ModelValidator<MenuModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}