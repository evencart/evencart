using EvenCart.Core.Caching;
using EvenCart.Infrastructure.ViewEngines.Expanders;

namespace EvenCart.Infrastructure.Caching
{
    public class CacheAccountant : ICacheAccountant
    {
        private readonly ICacheProvider _cacheProvider;
        public CacheAccountant(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public void PurgeCache()
        {
            //clear the provider cache
            _cacheProvider.Clear();
            //clear the view cache
            ReadFile.PurgeCache();
        }
    }
}