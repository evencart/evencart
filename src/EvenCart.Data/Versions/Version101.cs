using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<InviteRequest>(transaction);
            Db.AddColumn<UserCode, string>(nameof(UserCode.Email), null, transaction);
            Db.DropConstraint(Relation.Create<User, UserCode>("Id", "UserId"), transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
    
}