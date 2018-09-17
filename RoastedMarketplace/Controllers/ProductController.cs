using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;

namespace RoastedMarketplace.Controllers
{
    public class ProductController : FoundationController
    {
        [DualGet("{id:int}")]
        public IActionResult Index(int id)
        {
            return ContentResult("Nice try with" + id);
        }

        [CapabilityRequired("Protected")]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return ContentResult("Protected");
        }
    }
}