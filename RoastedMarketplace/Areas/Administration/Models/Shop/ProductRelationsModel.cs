using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
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