using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Navigation;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<Menu>(transaction);
            Db.CreateTable<MenuItem>(transaction);
            Db.CreateConstraint(Relation.Create<Menu, MenuItem>("Id", "MenuId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}