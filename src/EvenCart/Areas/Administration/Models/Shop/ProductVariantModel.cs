using System.Collections.Generic;
using System.Linq;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductVariantModel : FoundationEntityModel, IRequiresValidations<ProductVariantModel>
    {
        public IList<ProductAttributeModel> Attributes { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public decimal? Price { get; set; }

        public bool TrackInventory { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public void SetupValidationRules(ModelValidator<ProductVariantModel> v)
        {
            v.RuleFor(x => x.Attributes)
                .Must(x => x.Any(y => y.Id > 0))
                .WithMessage("At least one attribute combination must be passed to setup a variant.")
                .Must(x => x.Any(y => y.Values.Any(z => z.Id > 0)))
                .WithMessage("At least one attribute combination must be passed to setup a variant.");
        }
    }
}