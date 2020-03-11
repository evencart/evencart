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