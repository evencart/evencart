using System;
using System.Collections.Generic;
using NUnit.Framework;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Tests.Products
{
    public abstract class ProductServiceTests : BaseTest
    {
        private readonly IProductService _productService;
        private readonly IAvailableAttributeService _availableAttributeService;
        private readonly IAvailableAttributeValueService _availableAttributeValueService;
        private readonly IVendorService _vendorService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductVariantService _productVariantService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAccountant _productAccountant;
        protected ProductServiceTests()
        {
            _productAttributeService = Resolve<IProductAttributeService>();
            _productService = Resolve<IProductService>();
            _availableAttributeService = Resolve<IAvailableAttributeService>();
            _availableAttributeValueService = Resolve<IAvailableAttributeValueService>();
            _vendorService = Resolve<IVendorService>();
            _manufacturerService = Resolve<IManufacturerService>();
            _productVariantService = Resolve<IProductVariantService>();
            _productAccountant = Resolve<IProductAccountant>();
        }

        private void SeedData()
        {
            
        }

        [Test]
        public void Get_Product_By_Id_Succeeds()
        {
            var manufacturer = new Manufacturer() { Name = "RoastedBytes" };
            _manufacturerService.Insert(manufacturer);

            var vendor1 = new Vendor() {
                Name = "Vendor One"
            };
            var vendor2 = new Vendor() {
                Name = "Vendor One"
            };
            _vendorService.Insert(new[] { vendor1, vendor2 });

            var availableAttributes = new[]
            {
                new AvailableAttribute() {Name = "Size"},
                new AvailableAttribute() {Name = "Color"}
            };

            _availableAttributeService.Insert(availableAttributes);

            var attributeValues = new[]
            {
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 1,
                    Value = "S"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 1,
                    Value = "M"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 1,
                    Value = "L"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 2,
                    Value = "Red"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 2,
                    Value = "Green"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = 2,
                    Value = "Blue"
                },
            };

            _availableAttributeValueService.Insert(attributeValues);

            var product = new Product() {
                Name = "Microsoft Surface Pro",
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
                Sku = "SUR12",
                IsDownloadable = false,
                Description =
                    "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                Summary =
                    "Surface Pro delivers even more speed and performance thanks to a powerful Intel® Core™ processor — with up to 50% more battery life1 than Surface Pro 4 and 2.5x more performance than Surface Pro 3.",
                StockQuantity = 10,
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

            var productAttributeSize = new ProductAttribute() {
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

            var productAttributeColor = new ProductAttribute() {
                AvailableAttributeId = availableAttributes[1].Id,
                ProductId = product.Id,
                ProductAttributeValues = productAttributeValuesColor
            };

            _productAttributeService.Insert(productAttributeColor);

            //generate variations
            // s + red
            var sRedVariant = _productVariantService.AddVariant(product, new List<ProductVariantAttribute>()
            {
                new ProductVariantAttribute()
                {
                    ProductAttributeId = productAttributeSize.Id,
                    ProductAttributeValueId = productAttributeValuesSize[0].Id
                },
                new ProductVariantAttribute()
                {
                    ProductAttributeId = productAttributeColor.Id,
                    ProductAttributeValueId = productAttributeValuesColor[0].Id
                }
            });
            sRedVariant.TrackInventory = true;
            sRedVariant.StockQuantity = 5;
            _productVariantService.Update(sRedVariant);

            // s + green
            var sGreenVariant = _productVariantService.AddVariant(product, new List<ProductVariantAttribute>()
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
            });
            sGreenVariant.TrackInventory = false;
            sGreenVariant.StockQuantity = 0;
            _productVariantService.Update(sGreenVariant);
            // m + red
            var mRedVariant = _productVariantService.AddVariant(product, new List<ProductVariantAttribute>()
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
            });
            mRedVariant.TrackInventory = true;
            mRedVariant.StockQuantity = 0;
            _productVariantService.Update(mRedVariant);

            //link vendors
            _vendorService.AddVendorProduct(vendor1.Id, product.Id);
            _vendorService.AddVendorProduct(vendor2.Id, product.Id);

            //retrive the product
            var p = _productService.Get(product.Id);

            //same product?
            Assert.AreEqual(product.Id, p.Id);

            //attributes correct?
            Assert.AreEqual(2, p.ProductAttributes.Count);

            //attribute values correct
            Assert.AreEqual(3, p.ProductAttributes[0].ProductAttributeValues.Count);
            Assert.AreEqual(2, p.ProductAttributes[1].ProductAttributeValues.Count);

            //manufacturer right
            Assert.AreEqual(manufacturer.Id, p.Manufacturer.Id);

            //vendors right?
            Assert.AreEqual(2, p.Vendors.Count);

            //get variants
            var variants = _productVariantService.GetByProductId(product.Id);
            Assert.AreEqual(3, variants.Count);

            //single variants
            var singleVariant = _productVariantService.GetByAttributeValueIds(new List<int>()
            {
                productAttributeValuesSize[0].Id,
                productAttributeValuesColor[0].Id
            });
            Assert.AreEqual(sRedVariant.Id, singleVariant.Id);

            //single variants
            singleVariant = _productVariantService.GetByAttributeValueIds(new List<int>()
            {
                productAttributeValuesSize[0].Id,
                productAttributeValuesColor[1].Id
            });
            Assert.AreEqual(sGreenVariant.Id, singleVariant.Id);

            singleVariant = _productVariantService.GetByAttributeValueIds(new List<int>()
            {
                productAttributeValuesSize[1].Id,
                productAttributeValuesColor[0].Id
            });
            Assert.AreEqual(mRedVariant.Id, singleVariant.Id);

            //verify stocks
            var stockStatus = _productAccountant.GetStockStatus(product, new List<int>()
            {
                productAttributeValuesSize[0].Id,
                productAttributeValuesColor[0].Id
            }, out ProductVariant _);
            Assert.AreEqual(StockStatus.InStock, stockStatus);

            stockStatus = _productAccountant.GetStockStatus(product, new List<int>()
            {
                productAttributeValuesSize[0].Id,
                productAttributeValuesColor[1].Id
            }, out ProductVariant _);
            Assert.AreEqual(StockStatus.InStock, stockStatus);

            stockStatus = _productAccountant.GetStockStatus(product, new List<int>()
            {
                productAttributeValuesSize[1].Id,
                productAttributeValuesColor[0].Id
            }, out ProductVariant _);
            Assert.AreEqual(StockStatus.OutOfStock, stockStatus);

            stockStatus = _productAccountant.GetStockStatus(product, new List<int>()
            {
                productAttributeValuesSize[1].Id,
                productAttributeValuesColor[1].Id
            }, out ProductVariant _);
            Assert.AreEqual(StockStatus.Unavailable, stockStatus);

            //remove a product attribute value to see how variants are affected
            _productVariantService.DeleteVariantsByProductAttributeValueId(productAttributeValuesSize[0].Id);

            //get the variants again
            variants = _productVariantService.GetByProductId(product.Id);
            Assert.AreEqual(1, variants.Count);
            Assert.AreEqual(mRedVariant.Id, variants[0].Id);
        }

    }

    [TestFixture]
    public class SqlServerProductServiceTests : ProductServiceTests
    {
        public SqlServerProductServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}