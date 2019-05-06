using EvenCart.Core.Infrastructure;
using EvenCart.Infrastructure.MediaServices;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class GeneralSettingsModel : SettingsModel
    {
        public string StoreName { get; set; }

        public string StoreDomain { get; set; }

        public string DefaultTimeZoneId { get; set; }

        public int LogoId { get; set; }

        public string LogoUrl => DependencyResolver.Resolve<IMediaAccountant>().GetPictureUrl(LogoId);

        public int FaviconId { get; set; }

        public string FaviconUrl => DependencyResolver.Resolve<IMediaAccountant>().GetPictureUrl(FaviconId);

        public bool EnableBreadcrumbs { get; set; }

        public int PrimaryNavigationId { get; set; }

        public bool EnableJsBundling { get; set; }

        public bool EnableCssBundling { get; set; }

        public bool EnableHtmlMinification { get; set; }
    }
}