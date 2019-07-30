using EvenCart.Core.Caching;
using EvenCart.Infrastructure.Theming;
using EvenCart.Infrastructure.ViewEngines.Expanders;

namespace EvenCart.Infrastructure.Caching
{
    public class CacheAccountant : ICacheAccountant
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IThemeProvider _themeProvider;
        public CacheAccountant(ICacheProvider cacheProvider, IThemeProvider themeProvider)
        {
            _cacheProvider = cacheProvider;
            _themeProvider = themeProvider;
        }

        public void PurgeCache()
        {
            //clear the provider cache
            _cacheProvider.Clear();
            //clear the view cache
            ReadFile.PurgeCache();
            //clear the active theme template cache
            _themeProvider.ResetActiveTheme();
        }
    }
}