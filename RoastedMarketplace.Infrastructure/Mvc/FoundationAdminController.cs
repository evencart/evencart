using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Mvc
{
    [Route("[area]/[controller]")]
    [Area(ApplicationConfig.AdminAreaName)]
    public class FoundationAdminController : FoundationController
    {
        
    }
}