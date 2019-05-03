using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Purchases;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<Order, string>(nameof(Order.BillingAddressSerialized), null, transaction);
            Db.AddColumn<Order, string>(nameof(Order.ShippingAddressSerialized), null, transaction);
            Db.DropColumn<Order>("BillingAddressId", transaction);
            Db.DropColumn<Order>("ShippingAddressId", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
    
}