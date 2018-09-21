using DotEntity;
using DotEntity.Versioning;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version102 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            //Db.AddColumn<ProductCategory, int>(nameof(ProductCategory.DisplayOrder), 0, transaction);
            //Db.AddColumn<Category, int>(nameof(Category.MediaId), 0, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //Db.DropColumn<ProductCategory>(nameof(ProductCategory.DisplayOrder), transaction);
            //Db.DropColumn<Category>(nameof(Category.MediaId), transaction);
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version102";
    }
}