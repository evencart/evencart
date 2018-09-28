﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public static class RouteFinder
    {
        private static IList<RouteInfo> _routes;
        public static IList<RouteInfo> GetAllRoutes(string controller = null)
        {
            if (_routes == null)
            {
                var actionDescriptorCollectionProvider = DependencyResolver.Resolve<IActionDescriptorCollectionProvider>();
                _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items
                    .Where(x => x.AttributeRouteInfo.Name != null &&
                                !x.AttributeRouteInfo.Name.StartsWith(ApplicationConfig.ApiEndpointName))
                    .Select(x => new RouteInfo() {
                        Action = x.RouteValues["Action"],
                        Controller = x.RouteValues["Controller"],
                        Name = x.AttributeRouteInfo.Name,
                        Template = x.AttributeRouteInfo.Template
                    })
                    .ToList();
            }

            return !controller.IsNullEmptyOrWhitespace()
                ? _routes.Where(x => x.Controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase))
                    .ToList()
                : _routes;
        }
    }
}