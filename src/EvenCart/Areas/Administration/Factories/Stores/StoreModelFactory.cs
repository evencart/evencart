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

using EvenCart.Areas.Administration.Models.Store;
using Genesis.Modules.Settings;
using Genesis.Modules.Stores;

namespace EvenCart.Areas.Administration.Factories.Stores
{
    public class StoreModelFactory : IStoreModelFactory
    {
        private readonly ISettingService _settingService;
        public StoreModelFactory(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public StoreModel Create(Store entity)
        {
            //load the store's general settings
            var settings = entity.Id > 0 ? _settingService.GetSettings<GeneralSettings>(entity.Id) : null;
            var domain = settings?.StoreDomain ?? "";
            return new StoreModel()
            {
                Name = entity.Name + " (" + domain.Replace("//", "") + ")",
                Live = entity.Live,
                Id = entity.Id
            };
        }
    }
}