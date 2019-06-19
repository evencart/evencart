using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Shop
{
    public class Product : FoundationEntity, ISoftDeletable, ISeoEntity
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public bool IsShippable { get; set; }

        public bool IsDownloadable { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsVisibleIndividually { get; set; }

        public bool TrackInventory { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public decimal? ComparePrice { get; set; }

        public decimal Price { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public int MinimumPurchaseQuantity { get; set; } = 1;

        public int MaximumPurchaseQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int? ParentProductId { get; set; }

        public int DisplayOrder { get; set; }

        public bool ChargeTaxes { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int? ManufacturerId { get; set; }

        public int? TaxId { get; set; }

        public bool HasVariants { get; set; }

        public bool ReviewsDisabled { get; set; }

        public int PopularityIndex { get; set; }

        public decimal PackageWeight { get; set; }

        public WeightUnit PackageWeightUnit { get; set; }

        public decimal PackageWidth { get; set; }

        public LengthUnit PackageWidthUnit { get; set; }

        public decimal PackageHeight { get; set; }

        public LengthUnit PackageHeightUnit { get; set; }

        public decimal PackageLength { get; set; }

        public LengthUnit PackageLengthUnit { get; set; }

        public decimal AdditionalShippingCharge { get; set; }

        public bool IndividuallyShipped { get; set; }

        public bool AllowReturns { get; set; }

        public int DaysForReturn { get; set; }

        #region Virtual Properties

        public virtual IList<Category> Categories { get; set; }

        public virtual IList<ProductAttribute> ProductAttributes { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual IList<Vendor> Vendors { get; set; }

        public virtual IList<Media> MediaItems { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }

        public virtual ReviewSummaryData ReviewSummary { get; set; }

        public virtual IList<ProductSpecification> ProductSpecifications { get; set; }

        public virtual IList<ProductVariant> ProductVariants { get; set; }

        public virtual IList<WarehouseInventory> Inventories { get; set; }
        #endregion

        #region Relations
        public static Action<Product, Warehouse> WithWarehouse()
        {
            return (product, warehouse) =>
            {
                if (!product.HasVariants)
                {
                    foreach (var inventory in product.Inventories)
                    {
                        if (inventory.WarehouseId == warehouse.Id)
                            inventory.Warehouse = warehouse;
                    }
                }
                else
                {
                    var allInventories = product.ProductVariants.SelectMany(x => x.Inventories);
                    foreach (var inventory in allInventories)
                    {
                        if (inventory.WarehouseId == warehouse.Id)
                            inventory.Warehouse = warehouse;
                    }
                }
            };
        }

        public static Action<Product, Address> WithAddress()
        {
            return (product, address) =>
            {
                var warehouses = product.HasVariants ? product.ProductVariants.SelectMany(x => x.Inventories.Select(y => y.Warehouse))
                    : product.Inventories.Select(x => x.Warehouse);
                foreach (var warehouse in warehouses)
                {
                    if (warehouse.AddressId == address.Id)
                        warehouse.Address = address;
                }
            };
        }

        public static Action<Product, Country> WithCountry()
        {
            return (product, country) =>
            {
                var warehouses = product.HasVariants ? product.ProductVariants.SelectMany(x => x.Inventories.Select(y => y.Warehouse))
                    : product.Inventories.Select(x => x.Warehouse);
                foreach (var warehouse in warehouses)
                {
                    if (warehouse.Address.CountryId == country.Id)
                        warehouse.Address.Country = country;
                }
            };
        }

        #endregion

        #region Inner Classes

        public class ReviewSummaryData
        {
            public int TotalReviews { get; set; }

            public int TotalRatings { get; set; }

            public decimal AverageRating { get; set; }

            public int ProductId { get; set; }
        }
        #endregion
    }
}