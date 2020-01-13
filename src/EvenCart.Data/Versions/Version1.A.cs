using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Social;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1A : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityRole>(transaction);
            Db.AddColumn<Product, bool>(nameof(Product.RestrictedToRoles), false, transaction);
            Db.CreateTable<ConnectedAccount>(transaction);
            Db.CreateConstraint(Relation.Create<User, ConnectedAccount>("Id", "UserId"), transaction, true);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropConstraint(Relation.Create<User, ConnectedAccount>("Id", "UserId"), transaction);
            Db.DropTable<ConnectedAccount>(transaction);

            Db.DropColumn<Product>(nameof(Product.RestrictedToRoles), transaction);
            Db.DropTable<EntityRole>(transaction);
        }

        public string VersionKey => "EvenCart.Version.1A";
    }
}