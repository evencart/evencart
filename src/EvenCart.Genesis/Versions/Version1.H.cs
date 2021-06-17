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
using DotEntity;
using DotEntity.Versioning;
using Genesis.Modules.Localization;
using Genesis.Modules.Web;
using Db = DotEntity.DotEntity.Database;

namespace EvenCart.Data.Versions
{
    public class Version1H : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.CreateTable<Language>(transaction);
            Db.CreateTable<TranslationData>(transaction);
            Db.AddColumn<ContentPage, string>(nameof(ContentPage.TranslationGuid), String.Empty, transaction);
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropTable<TranslationData>(transaction);
            Db.DropTable<Language>(transaction);
        }

        public string VersionKey => "EvenCart.Version.1H";
    }
}