using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductRelationsModel : FoundationModel, IRequiresValidations<ProductRelationsModel>
    {
        public int ProductId { get; set; }

        public ProductRelationType RelationType { get; set; }

        public IList<int> DestinationProductIds { get; set; }

        public bool IsReciprocal { get; set; }

        public void SetupValidationRules(ModelValidator<ProductRelationsModel> v)
        {
            v.RuleFor(x => x.ProductId).GreaterThan(0);
            v.RuleFor(x => x.DestinationProductIds).Must(x => x.Any());
        }
    }
}