using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class ViewSplited : FoundationModel
    {
        public string BodyHtml { get; set; }

        public string Title { get; set; }

        public string HeadHtml { get; set; }

        public string Description { get; set; }
    }
}