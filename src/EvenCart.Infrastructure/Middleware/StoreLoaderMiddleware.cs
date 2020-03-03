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