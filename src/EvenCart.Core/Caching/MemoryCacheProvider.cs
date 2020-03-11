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

using System;
using Microsoft.Extensions.Caching.Memory;

namespace EvenCart.Core.Caching
{
    public class MemoryCacheProvider : FoundationCacheProvider<MemoryCache>
    {
        protected override MemoryCache InitializeCacheProvider()
        {
            return _CreateCacheObject();
        }

        public override T Get<T>(string cacheKey)
        {
            return (T) _cache.Get(cacheKey);
        }

        public override bool IsSet(string cacheKey)
        {
            return _cache.Get(cacheKey) != null;
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
           _cache.Set(cacheKey, cacheData, DateTimeOffset.Now.AddMinutes(expiration));
        }

        public override void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public override void Clear()
        {
           //dispose existing cache
           var existingCache = _cache;
           _cache = _CreateCacheObject();
           existingCache.Dispose();
        }

        #region Helpers
        private static MemoryCache _CreateCacheObject()
        {
            return new MemoryCache(new MemoryCacheOptions());
        } 
        #endregion
    }
}