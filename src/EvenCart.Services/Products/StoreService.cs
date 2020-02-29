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
        private readonly ICacheProvider _cacheProvider;
        public StoreService(ISettingService settingService, ICacheProvider cacheProvider)
        {
            _settingService = settingService;
            _cacheProvider = cacheProvider;
        }

        public Store GetByDomain(string domain)
        {
            var storeDomainKey = $"STORE_{domain}";
            return _cacheProvider.Get<Store>(storeDomainKey, () =>
            {
                //find a general setting with this domain
                if (!domain.StartsWith("//"))
                    domain = $"//{domain}";

                var setting = _settingService.FirstOrDefault(x =>
                    x.GroupName == nameof(GeneralSettings) && x.Key == nameof(GeneralSettings.StoreDomain) &&
                    x.Value == domain);
                if (setting == null)
                    return null;

                return Get(setting.StoreId);
            });
        }

        public void CloneStore(Store store, string newStoreName, string domain)
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
        }
    }
}