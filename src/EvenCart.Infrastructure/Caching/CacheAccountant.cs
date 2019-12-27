using EvenCart.Core.Caching;
using EvenCart.Infrastructure.Theming;
using EvenCart.Infrastructure.ViewEngines;
using EvenCart.Infrastructure.ViewEngines.Expanders;

namespace EvenCart.Infrastructure.Caching
{
    public class CacheAccountant : ICacheAccountant
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IThemeProvider _themeProvider;
        private readonly IViewAccountant _viewAccountant;
        public CacheAccountant(ICacheProvider cacheProvider, IThemeProvider themeProvider, IViewAccountant viewAccountant)
        {
            _cacheProvider = cacheProvider;
            _themeProvider = themeProvider;
            _viewAccountant = viewAccountant;
        }

        public void PurgeCache()
        {
            //clear the provider cache
            _cacheProvider.Clear();
            //clear the view cache
            ReadFile.PurgeCache();
            //clear the active theme template cache
            _themeProvider.ResetActiveTheme();
            //clear view location caches
            _viewAccountant.ClearCachedViews();
        }
    }
}