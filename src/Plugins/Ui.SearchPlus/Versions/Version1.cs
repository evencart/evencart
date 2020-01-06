using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.MediaEntities;
using Ui.SearchPlus.Data;
using Db = DotEntity.DotEntity.Database;
namespace Ui.SearchPlus.Versions
{
    public class Version1 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<SearchTerm>(transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropTable<SearchTerm>(transaction);
        }

        public string VersionKey { get; } = "Ui.SearchPlus.Version.1";
    }
}