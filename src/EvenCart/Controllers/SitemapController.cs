using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.Seo;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class SitemapController : FoundationController
    {
        private readonly ISitemapGenerator _sitemapGenerator;
        public SitemapController(ISitemapGenerator sitemapGenerator)
        {
            _sitemapGenerator = sitemapGenerator;
        }

        [HttpGet("~/sitemap.xml")]
        public IActionResult Index()
        {
            var siteMapXml = _sitemapGenerator.GetSitemapXml();
            return Content(siteMapXml, "application/xml");
        }
    }
}