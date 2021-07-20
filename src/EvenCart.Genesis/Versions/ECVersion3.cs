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

using System;
using System.Collections.Generic;
using DotEntity;
using DotEntity.MySql;
using DotEntity.SqlServer;
using EvenCart.Data.Entity.Shop;
using EvenCart.Genesis.Modules.Users;
using Genesis;
using Genesis.Data;
using Genesis.Database;

using Genesis.Modules.Users;
using Db = DotEntity.DotEntity.Database;

namespace EvenCart.Data.Versions
{
    public class ECVersion3 : GenesisVersion
    {
        public override void Upgrade(IDotEntityTransaction transaction)
        {
            base.Upgrade(transaction);

            Db.AddColumn<Download, int>(nameof(Download.DownloadCount), 0, transaction);
        }

        public override Type[] GetTables()
        {
            return Array.Empty<Type>();
        }

        public override Dictionary<Relation, bool> GetRelations()
        {
            return new Dictionary<Relation, bool>();
        }

        public override string VersionKey => "EvenCart.Version.3";
    }
}