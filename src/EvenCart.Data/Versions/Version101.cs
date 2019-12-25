using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            //Db.AddColumn<DiscountCoupon, decimal>(nameof(DiscountCoupon.MinimumOrderSubTotal), 0, transaction);
            Db.AddColumn<Vendor, VendorStatus>(nameof(Vendor.VendorStatus), VendorStatus.Active, transaction);
            Db.AddColumn<Vendor, bool>(nameof(Vendor.Deleted), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<DiscountCoupon>(nameof(DiscountCoupon.MinimumOrderSubTotal), transaction);
            Db.DropColumn<Vendor>(nameof(Vendor.VendorStatus), transaction);
            Db.DropColumn<Vendor>(nameof(Vendor.Deleted), transaction);
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
}