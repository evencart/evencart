namespace EvenCart.Core.Caching
{
    public abstract class FoundationCacheProvider<TCache> : ICacheProvider
    {
        protected TCache _cache;

        protected FoundationCacheProvider()
        {
            _cache = InitializeCacheProvider();
        }

        protected abstract TCache InitializeCacheProvider();

        public virtual void Dispose()
        {
            //do nothing..children can use if they want to do anything
        }

        public abstract T Get<T>(string cacheKey);

        public abstract bool IsSet(string cacheKey);

        public abstract void Set<T>(string cacheKey, T cacheData, int expiration = 60);

        public abstract void Remove(string cacheKey);

        public abstract void Clear();
    }
}