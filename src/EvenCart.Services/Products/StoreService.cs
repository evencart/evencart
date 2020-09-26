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
using EvenCart.Core.Caching;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Settings;

namespace EvenCart.Services.Products
{
    public class StoreService : FoundationEntityService<Store>, IStoreService
    {
        private readonly ISettingService _settingService;
        public StoreService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public Store GetByDomain(string domain, bool strictMatch = false)
        {
            var storeDomainKey = $"STORE_{domain}";
            return CacheProvider.Get<Store>(storeDomainKey, () =>
            {
                if (!strictMatch && domain.StartsWith("//"))
                {
                    domain = domain.Substring("//".Length);
                }
                var additionalMatch = !strictMatch && !domain.StartsWith("www.") ? $"www.{domain}" : domain.Substring("www.".Length);
                //find a general setting with this domain
                if (!domain.StartsWith("//"))
                {
                    domain = $"//{domain}";
                    additionalMatch = $"//{additionalMatch}";
                }
                
                Setting setting = null;
                if (!strictMatch)
                {
                   setting = _settingService.FirstOrDefault(x =>
                   x.GroupName == nameof(GeneralSettings) && x.Key == nameof(GeneralSettings.StoreDomain) &&
                   (x.Value == domain || x.Value == additionalMatch));
                }
                else
                {
                    setting = _settingService.FirstOrDefault(x =>
                    x.GroupName == nameof(GeneralSettings) && x.Key == nameof(GeneralSettings.StoreDomain) &&
                    x.Value == domain);
                }
                
                if (setting == null)
                    return null;

                return Get(setting.StoreId);
            });
        }

        public Store CloneStore(Store store, string newStoreName, string domain)
        {
            //get settings of source store
            var sourceStoreSettings = _settingService.Get(x => x.StoreId == store.Id).ToList();
            //create a new store
            var newStore = new Store()
            {
                Name = newStoreName
            };
            Insert(newStore);

            //update the domain for new setting
            var domainSetting = sourceStoreSettings.First(x =>
                x.Key == nameof(GeneralSettings.StoreDomain) && x.GroupName == nameof(GeneralSettings));

            domainSetting.Value = domain;
            Transaction.Initiate(transaction =>
            {
                //now insert all as new settings
                foreach (var setting in sourceStoreSettings)
                {
                    setting.StoreId = newStore.Id;
                    setting.Id = 0;
                    _settingService.Insert(setting, transaction);
                }
            });
            return newStore;
        }
    }
}