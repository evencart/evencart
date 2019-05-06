using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Services.Pages
{
    public interface ISeoMetaService : IFoundationEntityService<SeoMeta>
    {
        SeoMeta GetForEntity<T>(int entityId) where T : FoundationEntity;

        IList<SeoMeta> Search(string slug, string languageCultureCode = "en-US");
    }
}