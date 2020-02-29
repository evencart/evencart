using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Catalog
{
    public class CreateCatalogModel : FoundationModel, IRequiresValidations<CreateCatalogModel>
    {
        public string Name { get; set; }

        public int SourceStoreId { get; set; }

        public void SetupValidationRules(ModelValidator<CreateCatalogModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SourceStoreId).GreaterThan(0);
        }
    }
}