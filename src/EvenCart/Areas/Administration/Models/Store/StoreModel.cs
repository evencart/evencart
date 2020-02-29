using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Store
{
    public class StoreModel : FoundationEntityModel, IRequiresValidations<StoreModel>
    {
        public string Name { get; set; }

        public bool Live { get; set; }

        public void SetupValidationRules(ModelValidator<StoreModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}