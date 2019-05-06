using EvenCart.Core;
using EvenCart.Core.Data;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Pages;
using EvenCart.Services.Pages;

namespace EvenCart.Infrastructure.Consumers
{
    public class SeoEntityConsumer<T> : IFoundationEntityInserted<T>, IFoundationEntityDeleted<T> where T: FoundationEntity
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

        public void OnDeleted(T entity)
        {
            if (!(entity is ISeoEntity))
                return;
            var name = typeof(T).Name;
            var seoMeta = _seoMetaService.FirstOrDefault(x => x.EntityId == entity.Id && x.EntityName == name);
            if(seoMeta != null)
                _seoMetaService.Delete(seoMeta);
        }
    }
}