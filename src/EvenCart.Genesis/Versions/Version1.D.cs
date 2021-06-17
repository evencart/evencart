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
using EvenCart.Data.Entity.Purchases;
using Genesis.Modules.Navigation;
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