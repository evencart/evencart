#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Linq;
using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Shop;
using Genesis.Modules.Meta;
using Genesis.Modules.Navigation;
using Genesis.Modules.Web;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1C : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<EntityTag>(transaction);

            //execute the following only if it's not a fresh install
            if (transaction.CurrentlyRanVersions.All(x => x.GetType() != typeof(Version1)))
            {
                Db.AddColumn<MenuItem, bool>(nameof(MenuItem.OpenInNewWindow), false, transaction);
                var parentCategoryIdCol = DotEntityDb.Provider.SafeEnclose(nameof(Category.ParentCategoryId));
                var parentIdCol = DotEntityDb.Provider.SafeEnclose(nameof(Category.ParentId));
                var categoryTable = DotEntityDb.GetTableNameForType<Category>();
                Db.AddColumn<Category, int>(nameof(Category.ParentId), 0, transaction);
                Db.Query($"UPDATE {categoryTable} SET {parentIdCol}={parentCategoryIdCol}", null, transaction);
                Db.DropColumn<Category>(nameof(Category.ParentCategoryId), transaction);
                Db.AddColumn<ContentPage, int>(nameof(ContentPage.ParentId), 0, transaction);
            }
      
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {

            Db.DropTable<EntityTag>(transaction);
            Db.DropColumn<MenuItem>(nameof(MenuItem.OpenInNewWindow), transaction);

            var parentCategoryIdCol = DotEntityDb.Provider.SafeEnclose(nameof(Category.ParentCategoryId));
            var parentIdCol = DotEntityDb.Provider.SafeEnclose(nameof(Category.ParentId));
            var categoryTable = DotEntityDb.GetTableNameForType<Category>();

            Db.AddColumn<Category, int>(nameof(Category.ParentCategoryId), 0, transaction);
            Db.Query($"UPDATE {categoryTable} SET {parentCategoryIdCol}={parentIdCol}", null, transaction);
            Db.DropColumn<Category>(nameof(Category.ParentId), transaction);

            Db.DropColumn<ContentPage>(nameof(ContentPage.ParentId), transaction);
        }

        public string VersionKey => "EvenCart.Version.1C";
    }
}