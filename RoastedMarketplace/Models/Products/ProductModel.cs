using System;
using System.Collections.Generic;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Models.Media;

namespace RoastedMarketplace.Models.Products
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

        public decimal Price { get; set; }

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

        public string SeName { get; set; }

        public IList<MediaModel> Media { get; set; }

        public IList<ProductAttributeModel> ProductAttributes { get; set; }
    }
}