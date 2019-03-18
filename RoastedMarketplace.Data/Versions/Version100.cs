using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Logs;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Navigation;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Taxes;
using RoastedMarketplace.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version100 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            //user
            Db.CreateTable<User>(transaction);
            Db.CreateTable<Role>(transaction);
            Db.CreateTable<Capability>(transaction);
            Db.CreateTable<UserRole>(transaction);
            Db.CreateTable<RoleCapability>(transaction);
            Db.CreateTable<UserCapability>(transaction);
            Db.CreateTable<Vendor>(transaction);
            Db.CreateTable<VendorUser>(transaction);
            Db.CreateTable<Address>(transaction);
            Db.CreateTable<Country>(transaction);
            Db.CreateTable<StateOrProvince>(transaction);

            Db.CreateConstraint(Relation.Create<User, UserRole>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Role, UserRole>("Id", "RoleId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, Address>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Role, RoleCapability>("Id", "RoleId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Capability, RoleCapability>("Id", "CapabilityId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Capability, UserCapability>("Id", "CapabilityId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, UserCapability>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, VendorUser>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Vendor, VendorUser>("Id", "VendorId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Country, Address>("Id", "CountryId"), transaction, true);

            //shop
            Db.CreateTable<Product>(transaction);
            Db.CreateTable<Category>(transaction);
            Db.CreateTable<ProductCategory>(transaction);
            Db.CreateTable<AvailableAttribute>(transaction);
            Db.CreateTable<AvailableAttributeValue>(transaction);
            Db.CreateTable<ProductAttribute>(transaction);
            Db.CreateTable<ProductAttributeValue>(transaction);
            Db.CreateTable<Manufacturer>(transaction);
            Db.CreateTable<ProductVendor>(transaction);
            Db.CreateTable<ProductVariant>(transaction);
            Db.CreateTable<ProductVariantAttribute>(transaction);
            Db.CreateTable<Media>(transaction);
            Db.CreateTable<ProductMedia>(transaction);
            Db.CreateTable<Review>(transaction);
            Db.CreateTable<ProductSpecificationGroup>(transaction);
            Db.CreateTable<ProductSpecification>(transaction);
            Db.CreateTable<ProductSpecificationValue>(transaction);
            Db.CreateTable<ProductRelation>(transaction);

            

            Db.CreateConstraint(Relation.Create<Product, ProductCategory>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Category, ProductCategory>("Id", "CategoryId"), transaction, true);
            Db.CreateConstraint(Relation.Create<AvailableAttribute, AvailableAttributeValue>("Id", "AvailableAttributeId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductAttribute>("Id", "ProductId"), transaction);
            Db.CreateConstraint(Relation.Create<AvailableAttribute, ProductAttribute>("Id", "AvailableAttributeId"), transaction, true);
            Db.CreateConstraint(Relation.Create<AvailableAttributeValue, ProductAttributeValue>("Id", "AvailableAttributeValueId"), transaction);
            Db.CreateConstraint(Relation.Create<ProductAttribute, ProductAttributeValue>("Id", "ProductAttributeId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductVendor>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Vendor, ProductVendor>("Id", "VendorId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductVariant>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<ProductVariant, ProductVariantAttribute>("Id", "ProductVariantId"), transaction, true);
            Db.CreateConstraint(Relation.Create<ProductAttribute, ProductVariantAttribute>("Id", "ProductAttributeId"), transaction, true);
            Db.CreateConstraint(Relation.Create<ProductAttributeValue, ProductVariantAttribute>("Id", "ProductAttributeValueId"), transaction);
            Db.CreateConstraint(Relation.Create<Media, ProductMedia>("Id", "MediaId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductMedia>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, Review>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, Review>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Order, Review>("Id", "OrderId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductSpecificationGroup>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductSpecification>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<ProductSpecification, ProductSpecificationValue>("Id", "ProductSpecificationId"), transaction, true);
            Db.CreateConstraint(Relation.Create<AvailableAttribute, ProductSpecification>("Id", "AvailableAttributeId"), transaction, true);
            Db.CreateConstraint(Relation.Create<AvailableAttributeValue, ProductSpecificationValue>("Id", "AvailableAttributeValueId"), transaction);
            Db.CreateConstraint(Relation.Create<Product, ProductRelation>("Id", "SourceProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductRelation>("Id", "DestinationProductId"), transaction, true);

            Db.CreateTable<DiscountCoupon>(transaction);
            Db.CreateTable<RestrictionValue>(transaction);
            Db.CreateConstraint(Relation.Create<DiscountCoupon, RestrictionValue>("Id", "DiscountCouponId"), transaction, true);
            //tax
            Db.CreateTable<Tax>(transaction);
            Db.CreateTable<TaxRate>(transaction);
            Db.CreateConstraint(Relation.Create<Tax, TaxRate>("Id", "TaxId"), transaction, true);

            //cart
            Db.CreateTable<Cart>(transaction);
            Db.CreateTable<CartItem>(transaction);
            Db.CreateTable<Order>(transaction);
            Db.CreateTable<OrderItem>(transaction);
            Db.CreateTable<Shipment>(transaction);
            Db.CreateTable<ShipmentItem>(transaction);
            Db.CreateTable<PaymentTransaction>(transaction);

            //content
            Db.CreateTable<ContentPage>(transaction);
            Db.CreateConstraint(Relation.Create<User, ContentPage>("Id", "UserId"), transaction, true);

            //emails
            Db.CreateTable<EmailAccount>(transaction);
            Db.CreateTable<EmailTemplate>(transaction);
            Db.CreateTable<EmailMessage>(transaction);
            Db.CreateConstraint(Relation.Create<EmailAccount, EmailTemplate>("Id", "EmailAccountId"), transaction, false);

            Db.CreateConstraint(Relation.Create<Cart, CartItem>("Id", "CartId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, Cart>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Order, OrderItem>("Id", "OrderId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, Order>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, CartItem>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, OrderItem>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Shipment, ShipmentItem>("Id", "ShipmentId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Shipment, ShipmentHistory>("Id", "ShipmentId"), transaction, true);

            //menus
            Db.CreateTable<Menu>(transaction);
            Db.CreateTable<MenuItem>(transaction);
            Db.CreateConstraint(Relation.Create<Menu, MenuItem>("Id", "MenuId"), transaction, true);

            //old password and active codes
            Db.CreateTable<PreviousPassword>(transaction);
            Db.CreateTable<UserCode>(transaction);
            Db.CreateConstraint(Relation.Create<User, UserCode>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, PreviousPassword>("Id", "UserId"), transaction, true);

            Db.CreateTable<Currency>(transaction);

            //settings, logs, and others
            Db.CreateTable<Setting>(transaction);
            Db.CreateTable<Log>(transaction);
            Db.CreateTable<SeoMeta>(transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
           
            Db.DropConstraint(Relation.Create<User, UserRole>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Role, UserRole>("Id", "RoleId"), transaction);
            Db.DropConstraint(Relation.Create<User, Address>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Role, RoleCapability>("Id", "RoleId"), transaction);
            Db.DropConstraint(Relation.Create<Capability, RoleCapability>("Id", "CapabilityId"), transaction);
            Db.DropConstraint(Relation.Create<Capability, UserCapability>("Id", "CapabilityId"), transaction);
            Db.DropConstraint(Relation.Create<User, UserCapability>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<User, VendorUser>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Vendor, VendorUser>("Id", "VendorId"), transaction);
            Db.DropConstraint(Relation.Create<Country, Address>("Id", "CountryId"), transaction);

            Db.DropConstraint(Relation.Create<Product, ProductCategory>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Category, ProductCategory>("Id", "CategoryId"), transaction);
            Db.DropConstraint(Relation.Create<AvailableAttribute, AvailableAttributeValue>("Id", "AvailableAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductAttribute>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<AvailableAttribute, ProductAttribute>("Id", "AvailableAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttribute, ProductAttributeValue>("Id", "ProductAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductVendor>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Vendor, ProductVendor>("Id", "VendorId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductVariant>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<ProductVariant, ProductVariantAttribute>("Id", "ProductVariantId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttribute, ProductVariantAttribute>("Id", "ProductAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttributeValue, ProductVariantAttribute>("Id", "ProductAttributeValueId"), transaction);
            Db.DropConstraint(Relation.Create<Media, ProductMedia>("Id", "MediaId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductMedia>("Id", "ProductId"), transaction);

            Db.DropConstraint(Relation.Create<Tax, TaxRate>("Id", "TaxId"), transaction);

            Db.DropConstraint(Relation.Create<Cart, CartItem>("Id", "CartId"), transaction);
            Db.DropConstraint(Relation.Create<User, Cart>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Order, OrderItem>("Id", "OrderId"), transaction);
            Db.DropConstraint(Relation.Create<User, Order>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Product, CartItem>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Product, OrderItem>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Shipment, ShipmentItem>("Id", "ShipmentId"), transaction);

            //user
            Db.DropTable<User>(transaction);
            Db.DropTable<Role>(transaction);
            Db.DropTable<Capability>(transaction);
            Db.DropTable<UserRole>(transaction);
            Db.DropTable<RoleCapability>(transaction);
            Db.DropTable<UserCapability>(transaction);
            Db.DropTable<Vendor>(transaction);
            Db.DropTable<VendorUser>(transaction);
            Db.DropTable<Address>(transaction);
            Db.DropTable<Country>(transaction);
            Db.DropTable<StateOrProvince>(transaction);


            //shop
            Db.DropTable<Product>(transaction);
            Db.DropTable<Category>(transaction);
            Db.DropTable<ProductCategory>(transaction);
            Db.DropTable<AvailableAttribute>(transaction);
            Db.DropTable<AvailableAttributeValue>(transaction);
            Db.DropTable<ProductAttribute>(transaction);
            Db.DropTable<ProductAttributeValue>(transaction);
            Db.DropTable<Manufacturer>(transaction);
            Db.DropTable<ProductVendor>(transaction);
            Db.DropTable<ProductVariant>(transaction);
            Db.DropTable<ProductVariantAttribute>(transaction);
            Db.DropTable<Media>(transaction);
            Db.DropTable<ProductMedia>(transaction);

            //tax
            Db.DropTable<Tax>(transaction);
            Db.DropTable<TaxRate>(transaction);

            //cart
            Db.DropTable<Cart>(transaction);
            Db.DropTable<CartItem>(transaction);
            Db.DropTable<Order>(transaction);
            Db.DropTable<OrderItem>(transaction);
            Db.DropTable<Shipment>(transaction);
            Db.DropTable<ShipmentItem>(transaction);
            Db.DropTable<PaymentTransaction>(transaction);

            //settings, logs, and others
            Db.DropTable<Setting>(transaction);
            Db.DropTable<Log>(transaction);

        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_0";
    }
}