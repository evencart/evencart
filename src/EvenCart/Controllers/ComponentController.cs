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

using System.Linq;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Manages component operations for the website
    /// </summary>
    [Route("component")]
    public class ComponentController : FoundationController
    {
        /// <summary>
        /// Renders the component with provided component name
        /// </summary>
        /// <param name="componentName">The name of component to render</param>
        /// <response code="200">The html for the rendered component</response>
        [HttpPost("{componentName}", Name = RouteNames.RenderComponent)]
        public IActionResult Index(string componentName)
        {
            //pass any additional data as model to the view
            var model = ApplicationEngine.CurrentHttpContext.Request.Form?.ToDictionary(x => x.Key, x =>
            {
                if (x.Value.Count == 1)
                    return (object) x.Value[0];
                return (object) x.Value;
            });
            return ViewComponent(componentName, model);
        }
    }
}