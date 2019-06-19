using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version101 : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<ContentPage, string>(nameof(ContentPage.Template), "", transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            //do nothing
        }

        public string VersionKey => "EvenCart.Data.Versions.Version1_0_1";
    }
    
}