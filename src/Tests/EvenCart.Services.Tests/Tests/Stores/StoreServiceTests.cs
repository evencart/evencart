using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Settings;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Stores
{
    public abstract class StoreServiceTests : BaseTest
    {
        private IStoreService _storeService;
        private ISettingService _settingService;
        [SetUp]
        public void Setup()
        {
            _storeService = Resolve<IStoreService>();
            _settingService = Resolve<ISettingService>();
        }

        [Test]
        public void Store_Cloning_Succeeds()
        {
            var currentStore = ApplicationEngine.CurrentStore;
            var generalSettings = Resolve<GeneralSettings>();
            var defaultPageTitle = "Test Store Title";
            generalSettings.DefaultPageTitle = defaultPageTitle;
            _settingService.Save(generalSettings, currentStore.Id);

       
            var newStore = _storeService.CloneStore(currentStore, "New Store", "//newstore.com");
            Assert.Greater(newStore.Id, 0);
            Assert.AreEqual("New Store", newStore.Name);

            //change the default title
            generalSettings.DefaultPageTitle = "Changed Title";
            _settingService.Save(generalSettings, currentStore.Id);

            //set the active store to current store
            ApplicationEngine.CurrentHttpContext.SetCurrentStore(newStore);
            generalSettings = Resolve<GeneralSettings>();
            Assert.AreEqual(defaultPageTitle, generalSettings.DefaultPageTitle); //same title as the original store

            ApplicationEngine.CurrentHttpContext.SetCurrentStore(currentStore);
            generalSettings = Resolve<GeneralSettings>();
            Assert.AreEqual("Changed Title", generalSettings.DefaultPageTitle);

        }
    }
}