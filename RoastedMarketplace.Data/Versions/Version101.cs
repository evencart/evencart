using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.Shop;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<ProductRelation>(transaction);

            Db.CreateConstraint(Relation.Create<Product, ProductRelation>("Id", "SourceProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductRelation>("Id", "DestinationProductId"), transaction, false);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version1_0_1";
    }
    
}