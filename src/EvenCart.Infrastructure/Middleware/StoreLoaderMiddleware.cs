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

using System.Threading.Tasks;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class StoreLoaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStoreService _storeService;
        public StoreLoaderMiddleware(RequestDelegate next, IStoreService storeService)
        {
            _next = next;
            _storeService = storeService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (DatabaseManager.IsDatabaseInstalled())
            {
                var requestDomain = context.Request.Host.Value;
                var store = _storeService.GetByDomain(requestDomain);
                if (store == null)
                    return;
                context.SetCurrentStore(store);
            }
            await _next.Invoke(context);
        }
    }
}