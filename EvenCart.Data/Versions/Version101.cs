using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Gdpr;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
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

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
    
}