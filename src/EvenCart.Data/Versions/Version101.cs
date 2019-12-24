using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Promotions;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<DiscountCoupon, decimal>(nameof(DiscountCoupon.MinimumOrderSubTotal), 0, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<DiscountCoupon>(nameof(DiscountCoupon.MinimumOrderSubTotal), transaction);
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
}