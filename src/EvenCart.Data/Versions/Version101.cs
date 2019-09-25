using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.EntityProperties;
using EvenCart.Data.Entity.Users;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityProperty>(transaction);
            Db.CreateIndex<EntityProperty>(new[] { nameof(EntityProperty.EntityId) },
                transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }

}