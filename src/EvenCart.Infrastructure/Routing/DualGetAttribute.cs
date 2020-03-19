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

using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Infrastructure.Routing
{
    public class DualGetAttribute : HttpGetAttribute, IDualRouteAttribute
    {
        public DualGetAttribute(string template) : base(template)
        {
            
        }
        /// <summary>
        /// Specifies if the route should be available only as an API endpoint
        /// </summary>
        public bool OnlyApi { get; set; }
        /// <summary>
        /// Specifies if the route should be available only as a non-API endpoint.
        /// </summary>
        public bool OnlyNonApi { get; set; }
        /// <summary>
        /// Specifies if route should be available in headless mode. This is checked if the route is NON Api Route
        /// </summary>
        public bool AvailableInHeadlessMode { get; set; }
    }
}