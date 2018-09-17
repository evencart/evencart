using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Page
{
    public class SeoMeta : FoundationEntity
    {
        public string PageTitle { get; set; }
        
        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string Slug { get; set; }
    }
}