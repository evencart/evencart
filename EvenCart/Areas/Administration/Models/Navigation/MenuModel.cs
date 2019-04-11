using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Navigation
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