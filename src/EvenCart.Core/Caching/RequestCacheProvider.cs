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
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Core.Caching
{
    public class RequestCacheProvider : FoundationCacheProvider<Func<IDictionary<object, object>>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestCacheProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Func<IDictionary<object, object>> InitializeCacheProvider()
        {
            return () => _httpContextAccessor.HttpContext?.Items;
        }

        public override T Get<T>(string cacheKey)
        {
            return (T)Get(cacheKey, typeof(T));
        }

        public override object Get(string cacheKey, Type type)
        {
            var cache = _cache();
            if (cache == null)
                return default;

            cache.TryGetValue(cacheKey, out var obj);
            return obj;
        }

        public override bool IsSet(string cacheKey)
        {
            var cache = _cache();
            return cache != null && cache.ContainsKey(cacheKey);
        }

        public override void Set<T>(string cacheKey, T cacheData, int expiration = 60)
        {
            var cache = _cache();
            cache?.TryAdd(cacheKey, cacheData);
        }

        public override void Remove(string cacheKey)
        {
            var cache = _cache();
            if (cache == null)
                return;
            cache[cacheKey] = null;
        }

        public override void Clear()
        {
            var cache = _cache();
            cache?.Clear();
        }
    }
}