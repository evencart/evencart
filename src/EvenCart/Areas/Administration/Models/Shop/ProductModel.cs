﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Media;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Data.Entity.Shop;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Genesis.Modules.Data;
using Genesis.Modules.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductModel : GenesisEntityModel, IRequiresValidations<ProductModel>
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

        public bool ReviewsDisabled { get; set; }

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

        public ProductType ProductType { get; set; }

        public ProductSaleType ProductSaleType { get; set; }

        public TimeCycle SubscriptionCycle { get; set; }

        public int CycleCount { get; set; }

        public int? TrialDays { get; set; }

        public bool RequireLoginToPurchase { get; set; }

        public bool RequireLoginToViewPrice { get; set; }

        public IList<MediaModel> Media { get; set; }

        public IList<CategoryModel> Categories { get; set; }

        public SeoMetaModel SeoMeta { get; set; }

        public IList<int> RestrictedToRoleIds { get; set; }

        public IList<SelectListItem> RestrictedToRoles { get; set; }

        public IList<string> Tags { get; set; }

        public IList<SelectListItem> Catalogs { get; set; }

        public IList<int> CatalogIds { get; set; }

        public IList<int> VendorIds { get; set; }

        public bool DisableSale { get; set; }

        public void SetupValidationRules(ModelValidator<ProductModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.MinimumPurchaseQuantity).GreaterThan(0);
            v.RuleFor(x => x.RestrictedToRoles).Custom((list, context) =>
            {
                if (list != null && list.Any(x => int.TryParse(x.Value, out _) == false))
                {
                    context.AddFailure("Restricted to roles", LocalizationHelper.Localize("Invalid value for role id"));
                }
            });
        }
    }
}