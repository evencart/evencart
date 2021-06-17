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

using DotEntity.Versioning;
using EvenCart.Data.Versions;
using Genesis;

namespace EvenCart.Genesis
{
    public class DbVersionProvider : IDbVersionProvider
    {
        public IDatabaseVersion[] GetDatabaseVersions()
        {
            return new IDatabaseVersion[]
            {
                new Version1(),
                new Version1A(),
                new Version1B(),
                new Version1C(),
                new Version1D(),
                new Version1E(),
                new Version1F(),
                new Version1G(),
                new Version1H()
            };
        }
    }
}