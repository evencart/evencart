using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Shop;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version2 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityRole>(transaction);
            Db.AddColumn<Product, bool>(nameof(Product.RestrictedToRoles), false, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<Product>(nameof(Product.RestrictedToRoles), transaction);
            Db.DropTable<EntityRole>(transaction);
        }

        public string VersionKey => "EvenCart.Version.2";
    }
}