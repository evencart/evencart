using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Orders;
using EvenCart.Services.Products;
using Genesis.Modules.Addresses;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Purchases
{
    public abstract class OrderAccountantTests : BaseTest
    {
        private IWarehouseService _warehouseService;
        private IProductService _productService;
        private IWarehouseInventoryService _warehouseInventoryService;
        private IOrderAccountant _orderAccountant;
        private IAvailableAttributeService _availableAttributeService;
        private IAvailableAttributeValueService _availableAttributeValueService;
        private IProductAttributeService _productAttributeService;
        private IProductVariantService _productVariantService;
        private IAddressService _addressService;

        private AvailableAttribute _sizeAvailableAttribute, _colorAvailableAttribute;
        private Product _p1, _p2;
        private Warehouse _w1, _w2;
        private Order _o1, _o2, _o3, _o4;

        [OneTimeSetUp]
        protected void Setup()
        {
            _warehouseService = Resolve<IWarehouseService>();
            _warehouseInventoryService = Resolve<IWarehouseInventoryService>();
            _productService = Resolve<IProductService>();
            _orderAccountant = Resolve<IOrderAccountant>();
            _productAttributeService = Resolve<IProductAttributeService>();
            _availableAttributeService = Resolve<IAvailableAttributeService>();
            _availableAttributeValueService = Resolve<IAvailableAttributeValueService>();
            _productVariantService = Resolve<IProductVariantService>();
            _addressService = Resolve<IAddressService>();

            var address = new Address()
            {
                CountryId = 1,
                AddressType = AddressType.Home,
                Name = "abc"
            };
            _addressService.Insert(address);
            _w1 = new Warehouse() { AddressId = address.Id };
            _w2 = new Warehouse() { AddressId = address.Id };
            _warehouseService.Insert(_w1);
            _warehouseService.Insert(_w2);

            _sizeAvailableAttribute = new AvailableAttribute() {Name = "Size"};
            _colorAvailableAttribute = new AvailableAttribute() { Name = "Color"};
            var availableAttributes = new[]
            {
               _sizeAvailableAttribute, _colorAvailableAttribute
            };

            _availableAttributeService.Insert(availableAttributes);

            var attributeValues = new[]
            {
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _sizeAvailableAttribute.Id,
                    Value = "S"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _sizeAvailableAttribute.Id,
                    Value = "M"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _sizeAvailableAttribute.Id,
                    Value = "L"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _colorAvailableAttribute.Id,
                    Value = "Red"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _colorAvailableAttribute.Id,
                    Value = "Green"
                },
                new AvailableAttributeValue()
                {
                    AvailableAttributeId = _colorAvailableAttribute.Id,
                    Value = "Blue"
                },
            };

            _availableAttributeValueService.Insert(attributeValues);

            _p1 = new Product()
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

            _p2 = new Product()
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

            _productService.Insert(_p1);
            _productService.Insert(_p2);

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
                ProductId = _p1.Id,
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
                ProductId = _p1.Id,
                ProductAttributeValues = productAttributeValuesColor
            };

            _productAttributeService.Insert(productAttributeColor);


            //generate variations
            // s + red
            var sRedVariant = _productVariantService.AddVariant(_p1, new ProductVariant()
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
                ProductId = _p1.Id,
                ProductVariantId = sRedVariant.Id,
                TotalQuantity = 5,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
            });
            // s + green
            var sGreenVariant = _productVariantService.AddVariant(_p1, new ProductVariant()
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
                ProductId = _p1.Id,
                ProductVariantId = sGreenVariant.Id,
                TotalQuantity = 5,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
            });

            // m + red
            var mRedVariant = _productVariantService.AddVariant(_p1, new ProductVariant()
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
                ProductId = _p1.Id,
                ProductVariantId = mRedVariant.Id,
                TotalQuantity = 0,
                ReservedQuantity = 0,
                WarehouseId = _w1.Id
            });

            _warehouseInventoryService.Insert(new WarehouseInventory()
            {
                ProductId = _p2.Id,
                WarehouseId = _w1.Id,
                ReservedQuantity = 1,
                TotalQuantity = 1
            });
            _warehouseInventoryService.Insert(new WarehouseInventory()
            {
                ProductId = _p2.Id,
                WarehouseId = _w2.Id,
                ReservedQuantity = 1,
                TotalQuantity = 2
            });

            _o1 = new Order()
            {
                Id = 1,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 1,
                        ProductId = _p1.Id,
                        ProductVariantId = sRedVariant.Id,
                        Quantity = 2,
                        Id = 1
                    },
                    new OrderItem()
                    {
                        OrderId = 1,
                        ProductId = _p1.Id,
                        ProductVariantId = sGreenVariant.Id,
                        Quantity = 1,
                        Id = 2
                    }
                }
            };

            _o2 = new Order()
            {
                Id = 2,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 2,
                        ProductId = _p2.Id,
                        Quantity = 1,
                        Id = 3
                    }
                }
            };

            _o3 = new Order()
            {
                Id = 3,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 2,
                        ProductId = _p2.Id,
                        Quantity = 3,
                        Id = 4
                    }
                }
            };

            _o4 = new Order()
            {
                Id = 4,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderId = 1,
                        ProductId = _p1.Id,
                        ProductVariantId = sRedVariant.Id,
                        Quantity = 2,
                        Id = 5
                    },
                    new OrderItem()
                    {
                        OrderId = 1,
                        ProductId = _p1.Id,
                        ProductVariantId = sGreenVariant.Id,
                        Quantity = 1,
                        Id = 6
                    },
                    new OrderItem()
                    {
                        OrderId = 2,
                        ProductId = _p2.Id,
                        Quantity = 1,
                        Id = 7
                    }
                }
            };
        }

        [Test]
        public void Auto_Order_Fulfillments_Tests_Succeeds()
        {
            var fullFillments = _orderAccountant.GetAutoOrderFulfillments(_o1);
            Assert.AreEqual(2, fullFillments.Count);
            Assert.AreEqual(1, fullFillments[0].OrderItemId);
            Assert.AreEqual(_w1.Id, fullFillments[0].WarehouseId);
            Assert.AreEqual(2, fullFillments[1].OrderItemId);
            Assert.AreEqual(_w1.Id, fullFillments[1].WarehouseId);

            fullFillments = _orderAccountant.GetAutoOrderFulfillments(_o2);
            Assert.AreEqual(1, fullFillments.Count);
            Assert.AreEqual(3, fullFillments[0].OrderItemId);
            Assert.AreEqual(_w2.Id, fullFillments[0].WarehouseId);

            fullFillments = _orderAccountant.GetAutoOrderFulfillments(_o3);
            Assert.IsNull(fullFillments);

            fullFillments = _orderAccountant.GetAutoOrderFulfillments(_o4);
            Assert.AreEqual(3, fullFillments.Count);
            Assert.AreEqual(5, fullFillments[0].OrderItemId);
            Assert.AreEqual(_w1.Id, fullFillments[0].WarehouseId);
            Assert.AreEqual(6, fullFillments[1].OrderItemId);
            Assert.AreEqual(_w1.Id, fullFillments[1].WarehouseId);
            Assert.AreEqual(7, fullFillments[2].OrderItemId);
            Assert.AreEqual(_w2.Id, fullFillments[2].WarehouseId);
        }
    }
}