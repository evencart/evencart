#region License
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
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Factories.Products;
using EvenCart.Services.Products;
using Genesis;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Data;
using Genesis.Modules.Users;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "ProductAttributes")]
    public class ProductAttributesComponent : GenesisComponent
    {
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductVariantService _productVariantService;
        private readonly IPriceAccountant _priceAccountant;
        private readonly TaxSettings _taxSettings;
        public ProductAttributesComponent(IProductService productService, IProductModelFactory productModelFactory, IProductVariantService productVariantService, IPriceAccountant priceAccountant, TaxSettings taxSettings)
        {
            _productService = productService;
            _productModelFactory = productModelFactory;
            _productVariantService = productVariantService;
            _priceAccountant = priceAccountant;
            _taxSettings = taxSettings;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Fail.ComponentResult;

            dataAsDict.TryGetValue("productId", out var productIdObj);
            if (productIdObj == null || !int.TryParse(productIdObj.ToString(), out var productId))
                return R.Fail.ComponentResult;

            //get the product
            var product = _productService.Get(productId);
            if (product == null)
                return R.Fail.ComponentResult;
            if (!product.IsPublic(CurrentStore.Id) && !CurrentUser.Can(CapabilitySystemNames.EditProduct))
                return R.Fail.ComponentResult;
            var productModel = _productModelFactory.Create(product);

            var response = R.Success;
            response.With("product", productModel);

            if (product.HasVariants)
            {
                //any variants
                var variants = _productVariantService.GetByProductId(product.Id);
                var variantModels = new List<object>();
                foreach (var variant in variants)
                {
                    _priceAccountant.GetProductPriceDetails(product, null, variant.Price, out decimal priceWithoutTax, out decimal tax, out decimal taxRate, out _);
                    var variantObject = new
                    {
                        attributes = new Dictionary<string, string>(),
                        price = _priceAccountant
                            .ConvertCurrency(
                                (_taxSettings.DisplayProductPricesWithoutTax ? priceWithoutTax : priceWithoutTax + tax),
                                Engine.CurrentCurrency).ToCurrency(),
                        isAvailable = !variant.TrackInventory ||
                                      (variant.TrackInventory && variant.IsAvailableInStock(product)),
                        sku = !variant.Sku.IsNullEmptyOrWhiteSpace() ? variant.Sku : product.Sku,
                        gtin = !variant.Gtin.IsNullEmptyOrWhiteSpace() ? variant.Gtin : product.Gtin,
                        mpn = !variant.Mpn.IsNullEmptyOrWhiteSpace() ? variant.Mpn : product.Mpn
                    };
                    foreach (var pva in variant.ProductVariantAttributes)
                    {
                        variantObject.attributes.Add(pva.ProductAttribute.Label, pva.ProductAttributeValue.AvailableAttributeValue.Value);
                    }
                    variantModels.Add(variantObject);
                }

                if (variantModels.Any())
                    response.With("variants", variantModels);
                productModel.IsAvailable = variantModels.Any(x => (bool)((dynamic)x).isAvailable);
            }

            return response.ComponentResult;
        }
    }
}