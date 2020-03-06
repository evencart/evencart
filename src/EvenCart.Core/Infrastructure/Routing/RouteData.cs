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

using Microsoft.AspNetCore.Routing;

namespace EvenCart.Core.Infrastructure.Routing
{
    public partial class RouteData
    {
        public string RouteName { get; set; }

        public string Template { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public dynamic RouteValueDictionary { get; set; }

        public string SeoEntityName { get; set; }

        public int Order { get; set; }

        public string ParameterName { get; set; }

        public RouteValueDictionary GetRouteValueDictionary()
        {
            return (RouteValueDictionary) RouteValueDictionary;
        }
    }
}