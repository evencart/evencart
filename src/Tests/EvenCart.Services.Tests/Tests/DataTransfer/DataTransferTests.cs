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

using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;
using Genesis.Modules.DataTransfer;
using Genesis.Modules.Users;
using Genesis.Modules.Vendors;
using NUnit.Framework;

namespace EvenCart.Services.Tests.DataTransfer
{
    public abstract class DataTransferTests : BaseTest
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IUserService _userService;
        private IManufacturerService _manufacturerService;
        private IProductVariantService _productVariantService;
        private IProductAttributeService _productAttributeService;
        private IVendorService _vendorService;
        private IAvailableAttributeService _availableAttributeService;
        private IAvailableAttributeValueService _availableAttributeValueService;
        private IDataTransferManager _dataTransferManager;
        

        [SetUp]
        public void Setup()
        {
            _productService = Resolve<IProductService>();
            _categoryService = Resolve<ICategoryService>();
            _userService = Resolve<IUserService>();
            _manufacturerService = Resolve<IManufacturerService>();
            _productAttributeService = Resolve<IProductAttributeService>();
            _productVariantService = Resolve<IProductVariantService>();
            _vendorService = Resolve<IVendorService>();
            _availableAttributeService = Resolve<IAvailableAttributeService>();
            _availableAttributeValueService = Resolve<IAvailableAttributeValueService>();
            _dataTransferManager = Resolve<IDataTransferManager>();
        }

        [Test]
        public void ExcelProvider_Works()
        {
            InsertProducts(typeof(ExcelProvider).Name);
            var provider = Resolve<IDataTransferProvider<Product>>(typeof(ExcelProvider).FullName);
            RunTests(provider);
        }

        [Test]
        public void JsonProvider_Works()
        {
            InsertProducts(typeof(JsonProvider).Name);
            var provider = Resolve<IDataTransferProvider<Product>>(typeof(JsonProvider).FullName);
            RunTests(provider);
        }

        private void RunTests(IDataTransferProvider<Product> provider)
        {
            var providerName = provider.GetType().Name;
            //products
            var savedProductCount = _productService.Count();
            //export it
            var productsChunk = _dataTransferManager.Export<Product>(provider);

            //delete existing products
            var products = _productService.Get(x => x.Name == providerName).ToList();
            var productIds = products.Select(x => x.Id).ToList();
            var vendorIds = _vendorService.Get(x => x.Name == providerName).Select(x => x.Id).ToList();

            _productVariantService.Delete(x => productIds.Contains(x.ProductId));
            _productAttributeService.Delete(x => productIds.Contains(x.ProductId));
            _availableAttributeService.Delete(x => x.Name.EndsWith(providerName));
            _productService.Delete(x => x.Name == providerName);
            _manufacturerService.Delete(x => x.Name == providerName);
            _vendorService.Delete(x => vendorIds.Contains(x.Id));
            
            //now import it
            var importCount = _dataTransferManager.Import<Product>(productsChunk, provider);
            Assert.AreEqual(savedProductCount, importCount);

            products = _productService.Get(x => x.Name == providerName).ToList();

            Assert.AreEqual(1, products.Count());

            Assert.AreEqual(1, _manufacturerService.Count(x => x.Name == providerName));

            Assert.AreEqual(1, _vendorService.Count(x => x.Name == providerName));

            Assert.AreEqual(2, _availableAttributeService.Count(x => x.Name.EndsWith(providerName)));

            var product = products.Last(x => x.Name == providerName);
            //load with variants
            product = _productService.GetProductsWithVariants(new List<int>() {product.Id}).First();
            Assert.AreEqual(3, product.ProductVariants.Count);

            products = _productService.Get(x => x.Name == providerName).ToList();
            productIds = products.Select(x => x.Id).ToList();
            vendorIds = _vendorService.Get(x => x.Name == providerName).Select(x => x.Id).ToList();

            _productVariantService.Delete(x => productIds.Contains(x.ProductId));
            _productAttributeService.Delete(x => productIds.Contains(x.ProductId));
            _availableAttributeService.Delete(x => x.Name.EndsWith(providerName));
            _productService.Delete(x => x.Name == providerName);
            _manufacturerService.Delete(x => x.Name == providerName);
            _vendorService.Delete(x => vendorIds.Contains(x.Id));
        }

        private void InsertProducts(string providerName)
        {
            var manufacturer = new Manufacturer() { Name = providerName };
            _manufacturerService.Insert(manufacturer);

            var vendor1 = new Vendor()
            {
                Name = providerName
            };
            _vendorService.Insert(vendor1);

            var availableAttributes = new[]
            {
                new AvailableAttribute() {Name = "Size " + providerName},
                new AvailableAttribute() {Name = "Color " + providerName}
            };

            _availableAttributeService.Insert(availableAttributes);

            var attributeValues = new[]
            {
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[0].Id,
                    Value = "S"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[0].Id,
                    Value = "M"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[0].Id,
                    Value = "L"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[1].Id,
                    Value = "Red"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[1].Id,
                    Value = "Green"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = availableAttributes[1].Id,
                    Value = "Blue"
                },
            };

            _availableAttributeValueService.Insert(attributeValues);

            var product = new Product()
            {
                Name = providerName,
                CanOrderWhenOutOfStock = true,
                ChargeTaxes = true,
                ComparePrice = 100,
                Price = 80,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                IsShippable = true,
                IsFeatured = true,
                IsVisibleIndividually = true,
                DisplayOrder = 1,
                Mpn = "1",
                Gtin = "2",
                Sku = providerName,
                IsDownloadable = false,
                Description =
                    "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                Summary =
                    "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                Published = true,
                Deleted = false,
                TrackInventory = true,
                ManufacturerId = manufacturer.Id
            };

            _productService.Insert(product);

            //link product attributes
            var productAttributeValuesSize = new List<ProductAttributeValue>()
            {
                new ProductAttributeValue() {AvailableAttributeValueId = attributeValues[0].Id},
                new ProductAttributeValue() {AvailableAttributeValueId = attributeValues[1].Id},
                new ProductAttributeValue() {AvailableAttributeValueId = attributeValues[2].Id},
            };

            var productAttributeSize = new ProductAttribute()
            {
                AvailableAttributeId = availableAttributes[0].Id,
                ProductId = product.Id,
                ProductAttributeValues = productAttributeValuesSize
            };

            _productAttributeService.Insert(productAttributeSize);

            var productAttributeValuesColor = new List<ProductAttributeValue>()
            {
                new ProductAttributeValue() {AvailableAttributeValueId = attributeValues[3].Id},
                new ProductAttributeValue() {AvailableAttributeValueId = attributeValues[4].Id},
            };

            var productAttributeColor = new ProductAttribute()
            {
                AvailableAttributeId = availableAttributes[1].Id,
                ProductId = product.Id,
                ProductAttributeValues = productAttributeValuesColor
            };

            _productAttributeService.Insert(productAttributeColor);


            //generate variations
            // s + red
            var sRedVariant = _productVariantService.AddVariant(product, new ProductVariant()
            {
                ProductVariantAttributes = new List<ProductVariantAttribute>()
                {
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeSize.Id,
                        ProductAttributeValueId = productAttributeValuesSize[0].Id,
                    },
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeColor.Id,
                        ProductAttributeValueId = productAttributeValuesColor[0].Id
                    }
                },
                TrackInventory = true
            });
            // s + green
            var sGreenVariant = _productVariantService.AddVariant(product, new ProductVariant()
            {
                ProductVariantAttributes = new List<ProductVariantAttribute>()
                {
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeSize.Id,
                        ProductAttributeValueId = productAttributeValuesSize[0].Id
                    },
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeColor.Id,
                        ProductAttributeValueId = productAttributeValuesColor[1].Id
                    }
                },
                TrackInventory = false
            });

            // m + red
            var mRedVariant = _productVariantService.AddVariant(product, new ProductVariant()
            {
                ProductVariantAttributes = new List<ProductVariantAttribute>()
                {
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeSize.Id,
                        ProductAttributeValueId = productAttributeValuesSize[1].Id
                    },
                    new ProductVariantAttribute()
                    {
                        ProductAttributeId = productAttributeColor.Id,
                        ProductAttributeValueId = productAttributeValuesColor[0].Id
                    }
                },
                TrackInventory = true
            });

            //link vendors
            _vendorService.AddVendorProduct(vendor1.Id, product.Id);
        }
    }
}