using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Services.Cultures;

namespace EvenCart.Services.Helpers
{
    public static class CurrencyProviderHelper
    {
        public static ICurrencyRateProvider GetProvider(string providerName, bool returnDefault = true)
        {
            var allProviders = GetProviders();
            return allProviders.FirstOrDefault(x => x.ProviderSystemName == providerName) ??
                   allProviders.FirstOrDefault();
        }

        public static ICurrencyRateProvider[] GetProviders()
        {
            var providers = DependencyResolver.Resolve<ICurrencyRateProvider[]>();
            return providers;
        }
    }
}