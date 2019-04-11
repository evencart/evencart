using EvenCart.Core.Infrastructure;
using EvenCart.Infrastructure.Localization;

namespace EvenCart.Infrastructure.Helpers
{
    public static class LocalizationHelper
    {
        public static string Localize(string resource, string languageCultureCode, params object[] arguments)
        {
            if (resource == null)
                return null;
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            var localized = localizer.Localize(resource, languageCultureCode);
            return string.Format(localized, arguments);
        }
    }
}