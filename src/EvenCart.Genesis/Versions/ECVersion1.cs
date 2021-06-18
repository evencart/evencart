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
using DotEntity;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Taxes;
using Genesis.Data;
using Genesis.Modules.Addresses;
using Genesis.Modules.MediaServices;
using Genesis.Modules.Users;
using Genesis.Modules.Vendors;

namespace EvenCart.Data.Versions
{
    public class ECVersion1 : GenesisVersion
    {
        public override Type[] GetTables()
        {
            return new[]
            {
                typeof(AvailableAttribute),
                typeof(AvailableAttributeValue),
                typeof(Cart),
                typeof(CartItem),
                typeof(Category),
                typeof(Catalog),
                typeof(CatalogCountry),
                typeof(ProductCatalog),
                typeof(DiscountCoupon),
                typeof(Download),
                typeof(ItemDownload),
                typeof(Manufacturer),
                typeof(Order),
                typeof(OrderFulfillment),
                typeof(OrderItem),
                typeof(PaymentTransaction),
                typeof(Product),
                typeof(ProductAttribute),
                typeof(ProductAttributeValue),
                typeof(ProductCategory),
                typeof(ProductMedia),
                typeof(ProductRelation),
                typeof(ProductSpecification),
                typeof(ProductSpecificationGroup),
                typeof(ProductSpecificationValue),
                typeof(ProductVariant),
                typeof(ProductVariantAttribute),
                typeof(ProductVendor),
                typeof(RestrictionValue),
                typeof(ReturnRequest),
                typeof(Review),
                typeof(Shipment),
                typeof(ShipmentHistory),
                typeof(ShipmentItem),
                typeof(Tax),
                typeof(TaxRate),
                typeof(Upload),
                typeof(Vendor),
                typeof(VendorUser),
                typeof(Warehouse),
                typeof(WarehouseInventory),
            };
        }

        public override Dictionary<Relation, bool> GetRelations()
        {
            return new Dictionary<Relation, bool>()
            {
                {Relation.Create<Address, Warehouse>("Id", "AddressId"), true},
                {Relation.Create<AvailableAttribute, AvailableAttributeValue>("Id", "AvailableAttributeId"), true},
                {Relation.Create<AvailableAttribute, ProductAttribute>("Id", "AvailableAttributeId"), true},
                {Relation.Create<AvailableAttribute, ProductSpecification>("Id", "AvailableAttributeId"), true},
                {Relation.Create<AvailableAttributeValue, ProductAttributeValue>("Id", "AvailableAttributeValueId"), false},
                {Relation.Create<AvailableAttributeValue, ProductSpecificationValue>("Id", "AvailableAttributeValueId"), false},
                {Relation.Create<Cart, CartItem>("Id", "CartId"), true},
                {Relation.Create<Category, ProductCategory>("Id", "CategoryId"), true},
                {Relation.Create<DiscountCoupon, RestrictionValue>("Id", "DiscountCouponId"), true},
                {Relation.Create<Download, ItemDownload>("Id", "DownloadId"), true},
                {Relation.Create<Media, ProductMedia>("Id", "MediaId"), true},
                {Relation.Create<Order, OrderFulfillment>("Id", "OrderId"), true},
                {Relation.Create<Order, OrderItem>("Id", "OrderId"), true},
                {Relation.Create<Order, Review>("Id", "OrderId"), true},
                {Relation.Create<Product, CartItem>("Id", "ProductId"), true},
                {Relation.Create<Product, Download>("Id", "ProductId"), true},
                {Relation.Create<Product, OrderItem>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductAttribute>("Id", "ProductId"), false},
                {Relation.Create<Product, ProductCategory>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductMedia>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductRelation>("Id", "DestinationProductId"), false},
                {Relation.Create<Product, ProductRelation>("Id", "SourceProductId"), true},
                {Relation.Create<Product, ProductSpecification>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductSpecificationGroup>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductVariant>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductVendor>("Id", "ProductId"), true},
                {Relation.Create<Product, ProductCatalog>("Id", "ProductId"), true},
                {Relation.Create<Catalog, ProductCatalog>("Id", "CatalogId"), true},
                {Relation.Create<Product, Review>("Id", "ProductId"), true},
                {Relation.Create<Product, WarehouseInventory>("Id", "ProductId"), true},
                {Relation.Create<ProductAttribute, ProductAttributeValue>("Id", "ProductAttributeId"), true},
                {Relation.Create<ProductAttribute, ProductVariantAttribute>("Id", "ProductAttributeId"), true},
                {Relation.Create<ProductAttributeValue, ProductVariantAttribute>("Id", "ProductAttributeValueId"), false},
                {Relation.Create<ProductSpecification, ProductSpecificationValue>("Id", "ProductSpecificationId"), true},
                {Relation.Create<ProductVariant, ProductVariantAttribute>("Id", "ProductVariantId"), true},
                //{Relation.Create<ProductVariant, WarehouseInventory>("Id", "ProductVariantId"), false},
                {Relation.Create<Shipment, ShipmentHistory>("Id", "ShipmentId"), true},
                {Relation.Create<Shipment, ShipmentItem>("Id", "ShipmentId"), true},
                {Relation.Create<Tax, TaxRate>("Id", "TaxId"), true},
                {Relation.Create<User, Cart>("Id", "UserId"), true},
                {Relation.Create<User, Order>("Id", "UserId"), false},
                {Relation.Create<User, Review>("Id", "UserId"), true},
                {Relation.Create<User, Upload>("Id", "UserId"), true},
                {Relation.Create<User, VendorUser>("Id", "UserId"), true},
                {Relation.Create<Vendor, ProductVendor>("Id", "VendorId"), true},
                {Relation.Create<Vendor, VendorUser>("Id", "VendorId"), true},
                {Relation.Create<Warehouse, WarehouseInventory>("Id", "WarehouseId"), true},
            };
        }

        public override string VersionKey => "EvenCart.Version.1";
    }
}