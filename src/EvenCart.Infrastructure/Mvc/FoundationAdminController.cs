using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Infrastructure.Mvc
{
    [Route("[area]/[controller]")]
    [Area(ApplicationConfig.AdminAreaName)]
    [AuthorizeAdministrator]
    [Authorize]
    public class FoundationAdminController : FoundationController
    {
        
    }
}