﻿#region License
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
using DotEntity.Versioning;
using EvenCart.Data.Versions;
using Genesis;
using Genesis.App.Helpers;

namespace EvenCart.Genesis
{
    public class DbVersionProvider : IDbVersionProvider
    {
        public IDatabaseVersion[] GetDatabaseVersions()
        {
            return DbVersionProviderHelper.Merge(new GenesisCoreVersionProvider(), new GenesisAppVersionProvider())
                .Concat(new IDatabaseVersion[]
                {
                    new ECVersion1(),
                    new ECVersion2(),
                    new ECVersion3()
                }).ToArray();
        }
    }
}