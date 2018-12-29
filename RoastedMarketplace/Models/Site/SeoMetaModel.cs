using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Site
{
    public class SeoMetaModel : FoundationModel
    {
        public string PageTitle { get; set; }

        public string Description { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string LanguageCultureCode { get; set; }
    }
}