using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Page;

namespace RoastedMarketplace.Services.Pages
{
    public interface ISeoMetaService : IFoundationEntityService<SeoMeta>
    {
        SeoMeta GetForEntity<T>(int entityId) where T : FoundationEntity;
    }
}