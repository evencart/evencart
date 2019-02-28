using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects
{
    public class StoreObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var logoUrl = "";
            if (generalSettings.LogoId > 0)
            {
                logoUrl = mediaAccountant.GetPictureUrl(generalSettings.LogoId);
            }
            return new StoreImplementation()
            {
                Url = generalSettings.StoreDomain,
                Name = generalSettings.StoreName,
                Theme = new ThemeImplementation()
                {
                    Name = "Default",
                    Url = "/default"
                },
                LogoUrl = logoUrl,
                CurrentPage = ApplicationEngine.GetActiveRouteName()
            };
        }
    }
}