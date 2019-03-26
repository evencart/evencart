using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<PaymentTransaction, string>(nameof(PaymentTransaction.TransactionCodesSerialized), "", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}