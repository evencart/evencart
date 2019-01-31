using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Pages
{
    public class SeoMetaModel : FoundationEntityModel
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string Slug { get; set; }

        public string LanguageCultureCode { get; set; }
    }
}