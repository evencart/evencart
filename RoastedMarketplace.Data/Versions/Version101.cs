using DotEntity;
using DotEntity.Versioning;
using RoastedMarketplace.Data.Entity.MediaEntities;
using RoastedMarketplace.Data.Entity.Shop;
using Db = DotEntity.DotEntity.Database;
namespace RoastedMarketplace.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            //todo: think how to do this! On fresh install, this will create error
            //because it's trying to add columns which already have beeen created by
            //previous version

            //Db.AddColumn<Media, int>(nameof(Media.DisplayOrder), 0, transaction);
            //Db.AddColumn<Category, int>(nameof(Category.ParentCategoryId), 0, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //Db.DropColumn<Media>("DisplayOrder", transaction);
            //Db.DropColumn<Category>("ParentCategoryId", transaction);
        }

        public string VersionKey => "RoastedMarketplace.Data.Versions.Version101";
    }
}