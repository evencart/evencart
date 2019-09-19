using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<UserPoint>(transaction);
            Db.AddColumn<User, int>(nameof(User.Points), 0, transaction);
            Db.AddColumn<User, int?>(nameof(User.ProfilePictureId), 0, transaction);
            Db.CreateConstraint(Relation.Create<User, UserPoint>("Id", "UserId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
    
}