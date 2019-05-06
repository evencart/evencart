namespace EvenCart.Core.Caching
{
    //todo: implement redis cache provider
    public class RedisCacheProvider : FoundationCacheProvider<object>
    {
        private readonly RequestCacheProvider _requestCacheProvider;
        public RedisCacheProvider(RequestCacheProvider requestCacheProvider)
        {
            _requestCacheProvider = requestCacheProvider;
        }

        protected override object InitializeCacheProvider()
        {
            throw new System.NotImplementedException();
        }

        public override T Get<T>(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsSet(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}