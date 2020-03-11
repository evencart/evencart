#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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