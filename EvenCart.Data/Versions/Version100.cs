using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Entity.Logs;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Reviews;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Taxes;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
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

            //cart
            Db.CreateTable<Cart>(transaction);
            Db.CreateTable<CartItem>(transaction);
            Db.CreateTable<Order>(transaction);
            Db.CreateTable<OrderItem>(transaction);
            Db.CreateTable<Shipment>(transaction);
            Db.CreateTable<ShipmentItem>(transaction);
            Db.CreateTable<ShipmentHistory>(transaction);
            Db.CreateTable<PaymentTransaction>(transaction);

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
            Db.CreateConstraint(Relation.Create<Product, ProductRelation>("Id", "DestinationProductId"), transaction, false);

            Db.CreateTable<DiscountCoupon>(transaction);
            Db.CreateTable<RestrictionValue>(transaction);
            Db.CreateConstraint(Relation.Create<DiscountCoupon, RestrictionValue>("Id", "DiscountCouponId"), transaction, true);
            //tax
            Db.CreateTable<Tax>(transaction);
            Db.CreateTable<TaxRate>(transaction);
            Db.CreateConstraint(Relation.Create<Tax, TaxRate>("Id", "TaxId"), transaction, true);

           

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
            Db.CreateConstraint(Relation.Create<User, Order>("Id", "UserId"), transaction, false);
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

            //gdpr
            Db.CreateTable<Consent>(transaction);
            Db.CreateTable<ConsentLog>(transaction);
            Db.CreateTable<UserConsent>(transaction);
            Db.CreateTable<ConsentGroup>(transaction);
            Db.CreateConstraint(Relation.Create<Consent, ConsentLog>("Id", "ConsentId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Consent, UserConsent>("Id", "ConsentId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, UserConsent>("Id", "UserId"), transaction, true);

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
            Db.DropConstraint(Relation.Create<AvailableAttributeValue, ProductAttributeValue>("Id", "AvailableAttributeValueId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttribute, ProductAttributeValue>("Id", "ProductAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductVendor>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Vendor, ProductVendor>("Id", "VendorId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductVariant>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<ProductVariant, ProductVariantAttribute>("Id", "ProductVariantId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttribute, ProductVariantAttribute>("Id", "ProductAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<ProductAttributeValue, ProductVariantAttribute>("Id", "ProductAttributeValueId"), transaction);
            Db.DropConstraint(Relation.Create<Media, ProductMedia>("Id", "MediaId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductMedia>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<User, Review>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Product, Review>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Order, Review>("Id", "OrderId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductSpecificationGroup>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductSpecification>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<ProductSpecification, ProductSpecificationValue>("Id", "ProductSpecificationId"), transaction);
            Db.DropConstraint(Relation.Create<AvailableAttribute, ProductSpecification>("Id", "AvailableAttributeId"), transaction);
            Db.DropConstraint(Relation.Create<AvailableAttributeValue, ProductSpecificationValue>("Id", "AvailableAttributeValueId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductRelation>("Id", "SourceProductId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductRelation>("Id", "DestinationProductId"), transaction);
            Db.DropConstraint(Relation.Create<EmailAccount, EmailTemplate>("Id", "EmailAccountId"), transaction);
            Db.DropConstraint(Relation.Create<Cart, CartItem>("Id", "CartId"), transaction);
            Db.DropConstraint(Relation.Create<User, Cart>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Order, OrderItem>("Id", "OrderId"), transaction);
            Db.DropConstraint(Relation.Create<User, Order>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Product, CartItem>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Product, OrderItem>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Shipment, ShipmentItem>("Id", "ShipmentId"), transaction);
            Db.DropConstraint(Relation.Create<Shipment, ShipmentHistory>("Id", "ShipmentId"), transaction);
            Db.DropConstraint(Relation.Create<User, ContentPage>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<DiscountCoupon, RestrictionValue>("Id", "DiscountCouponId"), transaction);
            Db.DropConstraint(Relation.Create<Tax, TaxRate>("Id", "TaxId"), transaction);
            Db.DropConstraint(Relation.Create<User, UserCode>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<User, PreviousPassword>("Id", "UserId"), transaction);
            Db.DropConstraint(Relation.Create<Menu, MenuItem>("Id", "MenuId"), transaction);
            Db.DropConstraint(Relation.Create<Consent, ConsentLog>("Id", "ConsentId"), transaction);
            Db.DropConstraint(Relation.Create<Consent, UserConsent>("Id", "ConsentId"), transaction);
            Db.DropConstraint(Relation.Create<User, UserConsent>("Id", "UserId"), transaction);

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
            Db.DropTable<Review>(transaction);
            Db.DropTable<ProductSpecificationGroup>(transaction);
            Db.DropTable<ProductSpecification>(transaction);
            Db.DropTable<ProductSpecificationValue>(transaction);
            Db.DropTable<ProductRelation>(transaction);

            //cart
            Db.DropTable<Cart>(transaction);
            Db.DropTable<CartItem>(transaction);
            Db.DropTable<Order>(transaction);
            Db.DropTable<OrderItem>(transaction);
            Db.DropTable<Shipment>(transaction);
            Db.DropTable<ShipmentItem>(transaction);
            Db.DropTable<ShipmentHistory>(transaction);
            Db.DropTable<PaymentTransaction>(transaction);


            
            Db.DropTable<DiscountCoupon>(transaction);
            Db.DropTable<RestrictionValue>(transaction);

            //tax
           
            Db.DropTable<Tax>(transaction);
            Db.DropTable<TaxRate>(transaction);
            



            //content
            Db.DropTable<ContentPage>(transaction);
            

            //emails
            Db.DropTable<EmailAccount>(transaction);
            Db.DropTable<EmailTemplate>(transaction);
            Db.DropTable<EmailMessage>(transaction);
           

            //menus
            Db.DropTable<Menu>(transaction);
            Db.DropTable<MenuItem>(transaction);
            

            //old password and active codes
            Db.DropTable<PreviousPassword>(transaction);
            Db.DropTable<UserCode>(transaction);
          

            Db.DropTable<Currency>(transaction);

            //gdpr
            Db.DropTable<Consent>(transaction);
            Db.DropTable<ConsentLog>(transaction);
            Db.DropTable<UserConsent>(transaction);
            Db.DropTable<ConsentGroup>(transaction);


            //settings, logs, and others
            Db.DropTable<Setting>(transaction);
            Db.DropTable<Log>(transaction);
            Db.DropTable<SeoMeta>(transaction);


        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_0";
    }
}