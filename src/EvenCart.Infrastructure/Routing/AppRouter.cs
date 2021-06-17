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
using Genesis.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace EvenCart.Infrastructure.Routing
{
    public class AppRouter : IRouter
    {
        private readonly IRouter _defaultRouter;
        private readonly IDynamicRouteProvider _dynamicRouteProvider;
        public AppRouter(IRouter defaultRouter)
        {
            _defaultRouter = defaultRouter;
            _dynamicRouteProvider = DependencyResolver.Resolve<IDynamicRouteProvider>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            _dynamicRouteProvider.GetMatchingRoute(this, context);
            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return _dynamicRouteProvider.GetVirtualPathData(this, context);
        }
    }
}