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

namespace EvenCart.Core.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheProvider cacheProvider, string cacheKey, Func<T> retrieveFunc, int expiration = 60)
        {
            if (cacheProvider.IsSet(cacheKey))
                return cacheProvider.Get<T>(cacheKey);

            //retrive the value
            var tValue = retrieveFunc();

            //set this value
            cacheProvider.Set(cacheKey, tValue, expiration);
            return tValue;
        }
    }
}