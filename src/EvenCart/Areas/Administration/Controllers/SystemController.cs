using EvenCart.Areas.Administration.Factories.System;
using EvenCart.Areas.Administration.Models.System;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Allows admin to retrieve system information
    /// </summary>
    public class SystemController : FoundationAdminController
    {
        private readonly IAboutModelFactory _aboutModelFactory;
        public SystemController(IAboutModelFactory aboutModelFactory)
        {
            _aboutModelFactory = aboutModelFactory;
        }

        /// <summary>
        /// Gets the details about the software such as version, loaded assemblies etc. and system information such as available RAM, system time etc.
        /// </summary>
        /// <response code="200">A <see cref="AboutModel">info</see> object as 'info'</response>
        [DualGet("", Name = AdminRouteNames.GetAbout)]
        public IActionResult About()
        {
            var model = _aboutModelFactory.Create();
            return R.Success.With("info", model).Result;
        }
    }
}