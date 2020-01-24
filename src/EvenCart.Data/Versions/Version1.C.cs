using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1C : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityTag>(transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropTable<EntityTag>(transaction);
        }

        public string VersionKey => "EvenCart.Version.1C";
    }
}