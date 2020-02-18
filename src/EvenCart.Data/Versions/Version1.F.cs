using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Purchases;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1F : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<OrderFulfillment, bool>(nameof(OrderFulfillment.Locked), true, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<OrderFulfillment>(nameof(OrderFulfillment.Locked), transaction);
        }

        public string VersionKey => "EvenCart.Version.1F";
    }
}