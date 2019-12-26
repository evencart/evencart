using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            //Db.AddColumn<DiscountCoupon, decimal>(nameof(DiscountCoupon.MinimumOrderSubTotal), 0, transaction);
            //Db.AddColumn<Vendor, VendorStatus>(nameof(Vendor.VendorStatus), VendorStatus.Active, transaction);
            //Db.AddColumn<Vendor, bool>(nameof(Vendor.Deleted), false, transaction);
            //Db.AddColumn<Product, bool>(nameof(Product.RequireLoginToPurchase), false, transaction);
            Db.AddColumn<Product, bool>(nameof(Product.RequireLoginToViewPrice), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<DiscountCoupon>(nameof(DiscountCoupon.MinimumOrderSubTotal), transaction);
            Db.DropColumn<Vendor>(nameof(Vendor.VendorStatus), transaction);
            Db.DropColumn<Vendor>(nameof(Vendor.Deleted), transaction);
            Db.DropColumn<Product>(nameof(Product.RequireLoginToPurchase), transaction);
            Db.DropColumn<Product>(nameof(Product.RequireLoginToViewPrice), transaction);
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
}