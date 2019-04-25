using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Media;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductModel : FoundationEntityModel, IRequiresValidations<ProductModel>
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public bool IsShippable { get; set; }

        public bool IsDownloadable { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsVisibleIndividually { get; set; }

        public bool TrackInventory { get; set; }

        public int StockQuantity { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public decimal? ComparePrice { get; set; }

        public string ComparePriceFormatted => ComparePrice.ToCurrency();

        public decimal Price { get; set; }

        public string PriceFormatted => Price.ToCurrency();

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public int MinimumPurchaseQuantity { get; set; }

        public int MaximumPurchaseQuantity { get; set; }

        public int? ParentProductId { get; set; }

        public int DisplayOrder { get; set; }

        public bool ChargeTaxes { get; set; }

        public bool Published { get; set; }

        public int? ManufacturerId { get; set; }

        public int? TaxId { get; set; }

        public string ManufacturerName { get; set; }

        public bool HasVariants { get; set; }

        public IList<MediaModel> Media { get; set; }

        public IList<CategoryModel> Categories { get; set; }

        public SeoMetaModel SeoMeta { get; set; }

        public void SetupValidationRules(ModelValidator<ProductModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}