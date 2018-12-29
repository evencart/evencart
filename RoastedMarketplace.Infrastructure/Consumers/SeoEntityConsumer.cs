using RoastedMarketplace.Core;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Entity.Page;
using RoastedMarketplace.Services.Pages;

namespace RoastedMarketplace.Infrastructure.Consumers
{
    public class SeoEntityConsumer<T> : IFoundationEntityInserted<T> where T: FoundationEntity
    {
        private readonly ISeoMetaService _seoMetaService;
        public SeoEntityConsumer(ISeoMetaService seoMetaService)
        {
            _seoMetaService = seoMetaService;
        }

        public void OnInserted(T entity)
        {
            if (!(entity is ISeoEntity))
                return;
            var name = ((ISeoEntity) entity).Name;
            var seoMeta = new SeoMeta()
            {
                EntityId = entity.Id,
                EntityName = typeof(T).Name,
                LanguageCultureCode = ApplicationEngine.CurrentLanguageCultureCode,
                Slug = CommonHelper.GenerateSlug(name)
            };
            _seoMetaService.Insert(seoMeta);
        }
    }
}