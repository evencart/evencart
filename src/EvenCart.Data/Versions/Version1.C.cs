using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Navigation;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1C : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityTag>(transaction);
            Db.AddColumn<MenuItem, bool>(nameof(MenuItem.OpenInNewWindow), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropTable<EntityTag>(transaction);
            Db.DropColumn<MenuItem>(nameof(MenuItem.OpenInNewWindow), transaction);
        }

        public string VersionKey => "EvenCart.Version.1C";
    }
}