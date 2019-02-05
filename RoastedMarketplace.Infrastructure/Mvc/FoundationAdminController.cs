using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Security.Attributes;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    [Route("[area]/[controller]")]
    [Area(ApplicationConfig.AdminAreaName)]
    [AuthorizeAdministrator]
    public class FoundationAdminController : FoundationController
    {
        
    }
}