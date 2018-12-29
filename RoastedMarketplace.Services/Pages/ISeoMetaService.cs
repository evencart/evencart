using System.Collections.Generic;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Page;

namespace RoastedMarketplace.Services.Pages
{
    public interface ISeoMetaService : IFoundationEntityService<SeoMeta>
    {
        SeoMeta GetForEntity<T>(int entityId) where T : FoundationEntity;

        IList<SeoMeta> Search(string slug, string languageCultureCode = "en-US");
    }
}