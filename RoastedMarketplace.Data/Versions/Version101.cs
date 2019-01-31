using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateConstraint(Relation.Create<User, ContentPage>("Id", "UserId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}