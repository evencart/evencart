using EvenCart.Areas.Administration.Models.Store;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Settings;

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