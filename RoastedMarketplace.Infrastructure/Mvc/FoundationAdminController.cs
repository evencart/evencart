using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Security.Attributes;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    [Route("[area]/[controller]")]
    [Area(ApplicationConfig.AdminAreaName)]
    [AuthorizeAdministrator]
    [Authorize]
    public class FoundationAdminController : FoundationController
    {
        
    }
}