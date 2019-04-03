using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Gdpr;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<ConsentLog, string>(nameof(ConsentLog.EncryptedUserInfo), "", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}