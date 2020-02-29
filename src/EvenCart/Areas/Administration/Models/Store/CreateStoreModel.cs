using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Store
{
    public class CreateStoreModel : FoundationModel, IRequiresValidations<CreateStoreModel>
    {
        public string Name { get; set; }

        public string Domain { get; set; }

        public int SourceStoreId { get; set; }

        public void SetupValidationRules(ModelValidator<CreateStoreModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SourceStoreId).GreaterThan(0);
            v.RuleFor(x => x.Domain).NotEmpty().Must(x => x.StartsWith(@"//"));
        }
    }
}