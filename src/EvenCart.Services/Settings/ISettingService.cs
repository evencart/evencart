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
using EvenCart.Core.Config;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Settings;

namespace EvenCart.Services.Settings
{
    public interface ISettingService : IFoundationEntityService<Setting>
    {
        Setting Get<T>(string keyName, int storeId) where T : ISettingGroup;

        void Save<T>(string keyName, string keyValue, int storeId) where T : ISettingGroup;

        void Save<T>(T settings, int storeId) where T: ISettingGroup;

        void Save(Type settingType, object settings, int storeId);

        void Save(Type settingType, string keyName, string keyValue, int storeId);

        T GetSettings<T>(int storeId) where T : ISettingGroup;

        T GetSettings<T>(Func<int> getStore) where T : ISettingGroup;

        object GetSettings(Type settingType, int storeId);

        object GetSettings(Type settingType, Func<int> getStore);

        void LoadSettings<T>(T settingsObject, int storeId) where T : ISettingGroup;

        void DeleteSettings<T>(int storeId) where T : ISettingGroup;
    }
}