using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Navigation;
using RoastedMarketplace.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<PreviousPassword>(transaction);
            Db.CreateTable<UserCode>(transaction);
            Db.CreateConstraint(Relation.Create<User, UserCode>("Id", "UserId"), transaction, true);
            Db.CreateConstraint(Relation.Create<User, PreviousPassword>("Id", "UserId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}