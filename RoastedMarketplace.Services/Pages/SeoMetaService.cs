using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Pages;

namespace RoastedMarketplace.Services.Pages
{
    public class SeoMetaService : FoundationEntityService<SeoMeta>, ISeoMetaService
    {
        public SeoMeta GetForEntity<T>(int entityId) where T : FoundationEntity
        {
            var entityName = typeof(T).Name;
            return Repository.Where(x => x.EntityName == entityName && x.EntityId == entityId).SelectSingle();
        }

        public IList<SeoMeta> Search(string slug, string languageCultureCode = "en-US")
        {
            return Repository.Where(x => x.Slug == slug && x.LanguageCultureCode == languageCultureCode)
                .Select()
                .ToList();
        }
    }
}