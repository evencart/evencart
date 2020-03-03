using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Store;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Catalog
{
    public class CatalogModel : FoundationEntityModel, IRequiresValidations<CatalogModel>
    {
        public string Name { get; set; }

        public IList<int> StoreIds { get; set; }

        public void SetupValidationRules(ModelValidator<CatalogModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}