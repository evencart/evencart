using System.Linq;
using DotEntity;
using DotEntity.Versioning;
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Purchases;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1D : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<MenuItem, string>(nameof(MenuItem.Description), "", transaction);
            Db.AddColumn<MenuItem, string>(nameof(MenuItem.ExtraData), "", transaction);
            //execute the following only if it's not a fresh install
            if (transaction.CurrentlyRanVersions.All(x => x.GetType() != typeof(Version1)))
            {
                var parentMenuItemIdCol = DotEntityDb.Provider.SafeEnclose("ParentMenuItemId");
                var parentIdCol = DotEntityDb.Provider.SafeEnclose(nameof(MenuItem.ParentId));
                var menuItemTable = DotEntityDb.GetTableNameForType<MenuItem>();
                Db.AddColumn<MenuItem, int>(nameof(MenuItem.ParentId), 0, transaction);
                Db.Query($"UPDATE {menuItemTable} SET {parentIdCol}={parentMenuItemIdCol}", null, transaction);
                Db.DropColumn<MenuItem>("ParentMenuItemId", transaction);
            }

            var cartTable = DotEntityDb.Provider.SafeEnclose(DotEntityDb.GetTableNameForType<Cart>());
            var orderTable = DotEntityDb.Provider.SafeEnclose(DotEntityDb.GetTableNameForType<Order>());
            var selectedShippingOptionCol = DotEntityDb.Provider.SafeEnclose(nameof(Cart.SelectedShippingOption));
            var query =
                $"UPDATE {cartTable} SET {selectedShippingOptionCol}='[{{\"name\":\"' + {selectedShippingOptionCol} + '\"}}]'";
            Db.Query(query, null, transaction);
            query =
                $"UPDATE {orderTable} SET {selectedShippingOptionCol}='[{{\"name\":\"' + {selectedShippingOptionCol} + '\"}}]'";
            Db.Query(query, null, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<MenuItem>(nameof(MenuItem.Description), transaction);
            Db.DropColumn<MenuItem>(nameof(MenuItem.ExtraData), transaction);

            var parentMenuItemIdCol = DotEntityDb.Provider.SafeEnclose("ParentMenuItemId");
            var parentIdCol = DotEntityDb.Provider.SafeEnclose(nameof(MenuItem.ParentId));
            var menuItemTable = DotEntityDb.GetTableNameForType<MenuItem>();

            Db.AddColumn<MenuItem, int>("ParentMenuItemId", 0, transaction);
            Db.Query($"UPDATE {menuItemTable} SET {parentMenuItemIdCol}={parentIdCol}", null, transaction);
            Db.DropColumn<MenuItem>(nameof(MenuItem.ParentId), transaction);
        }

        public string VersionKey => "EvenCart.Version.1D";
    }
}