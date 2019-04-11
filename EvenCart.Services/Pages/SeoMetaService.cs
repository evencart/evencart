using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Services.Pages
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