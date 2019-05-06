using System;
using System.Collections.Generic;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Models.Media;
using EvenCart.Models.Reviews;

namespace EvenCart.Models.Products
{
    public class ProductModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public bool IsShippable { get; set; }

        public bool IsDownloadable { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsVisibleIndividually { get; set; }

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

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int DisplayOrder { get; set; }

        public bool ChargeTaxes { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int? ManufacturerId { get; set; }

        public int? TaxId { get; set; }

        public bool HasVariants { get; set; }

        public bool IsAvailable { get; set; }

        public string SeName { get; set; }

        public ReviewSummaryModel ReviewSummary { get; set; }

        public IList<MediaModel> Media { get; set; }

        public IList<ProductAttributeModel> ProductAttributes { get; set; }

        public IList<ProductSpecificationGroupModel> ProductSpecificationGroups { get; set; }
    }
}