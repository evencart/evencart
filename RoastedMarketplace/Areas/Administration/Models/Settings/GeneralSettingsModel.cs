using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.MediaServices;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class GeneralSettingsModel : SettingsModel
    {
        public string StoreDomain { get; set; }

        public string DefaultTimeZoneId { get; set; }

        public int LogoId { get; set; }

        public string LogoUrl => DependencyResolver.Resolve<IMediaAccountant>().GetPictureUrl(LogoId);
    }
}