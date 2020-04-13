using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Addresses;
using EvenCart.Services.Common;
using EvenCart.Services.Products;
using EvenCart.Services.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Products
{
    public abstract class ProductServiceTests : BaseTest
    {
        private IProductService _productService;
        private IAvailableAttributeService _availableAttributeService;
        private IAvailableAttributeValueService _availableAttributeValueService;
        private IVendorService _vendorService;
        private IManufacturerService _manufacturerService;
        private IProductVariantService _productVariantService;
        private IProductAttributeService _productAttributeService;
        private IProductAccountant _productAccountant;
        private IWarehouseService _warehouseService;
        private IWarehouseInventoryService _warehouseInventoryService;
        private IAddressService _addressService;
        private IEntityRoleService _entityRoleService;
        private Warehouse _w1, _w2;

        [SetUp]
        public void Setup()
        {
            _productAttributeService = Resolve<IProductAttributeService>();
            _productService = Resolve<IProductService>();
            _availableAttributeService = Resolve<IAvailableAttributeService>();
            _availableAttributeValueService = Resolve<IAvailableAttributeValueService>();
            _vendorService = Resolve<IVendorService>();
            _manufacturerService = Resolve<IManufacturerService>();
            _productVariantService = Resolve<IProductVariantService>();
            _productAccountant = Resolve<IProductAccountant>();
            _warehouseService = Resolve<IWarehouseService>();
            _warehouseInventoryService = Resolve<IWarehouseInventoryService>();
            _addressService = Resolve<IAddressService>();
            _entityRoleService = Resolve<IEntityRoleService>();

            var address = new Address()
            {
                CountryId = 1,
                AddressType = AddressType.Home,
                Name = "abc"
            };
            _addressService.Insert(address);
            _w1 = new Warehouse() { AddressId = address.Id };
            _w2 = new Warehouse() { AddressId = address.Id };
            _warehouseService.Insert(new[] { _w1, _w2 });
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
            _warehouseInventoryService.Insert(new WarehouseInventory()
            {
                ProductId = product.Id,
                ProductVariantId = sRedVariant.Id,
                TotalQuantity = 5,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
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
            _warehouseInventoryService.Insert(new WarehouseInventory()
            {
                ProductId = product.Id,
                ProductVariantId = sGreenVariant.Id,
                TotalQuantity = 5,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
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
            _warehouseInventoryService.Insert(new WarehouseInventory()
            {
                ProductId = product.Id,
                ProductVariantId = mRedVariant.Id,
                TotalQuantity = 0,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
            });

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

        [Test]
        public void Role_Based_Products_Succeeds()
        {
            var product1 = new Product()
            {
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
                Published = true,
                Deleted = false,
                TrackInventory = true,
            };
            var product2 = new Product()
            {
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
                Published = true,
                Deleted = false,
                TrackInventory = true,
            };
            _productService.Insert(product1);
            _productService.Insert(product2);
            _entityRoleService.Insert(new EntityRole()
            {
                EntityId = product1.Id,
                EntityName = nameof(Product),
                RoleId = 1
            });

            //retrieve now
            var products = _productService.GetProducts(out _, out _, out _, out _, out _, out _, roleIds: new List<int>()
            {
                1
            });

            Assert.GreaterOrEqual(products.Count, 2);

            products = _productService.GetProducts(out _, out _, out _, out _, out _, out _, roleIds: new List<int>()
            {
                1
            }, ignoreRoles: true);
            Assert.GreaterOrEqual(products.Count, 2);

            products = _productService.GetProducts(out _, out _, out _, out _, out _, out _, roleIds: new List<int>()
            {
                2
            });
            Assert.GreaterOrEqual(products.Count, 1); //only product2 should be available
        }
    }
}