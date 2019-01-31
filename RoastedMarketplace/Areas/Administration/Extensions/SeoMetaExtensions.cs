using RoastedMarketplace.Areas.Administration.Models.Pages;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Services.Pages;

namespace RoastedMarketplace.Areas.Administration.Extensions
{
    public static class SeoMetaExtensions
    {
        public static void UpdateSeoMetaForEntity<T>(this ISeoMetaService seoMetaService, T entity, SeoMetaModel seoMetaModel) where T : FoundationEntity
        {
            var seoMeta = seoMetaService.FirstOrDefault(
                x => x.EntityId == entity.Id && x.EntityName == typeof(T).Name);
            if (seoMeta != null)
            {
                seoMeta.PageTitle = seoMetaModel.PageTitle;
                seoMeta.MetaKeywords = seoMetaModel.MetaKeywords;
                seoMeta.MetaDescription = seoMetaModel.MetaDescription;
                //check if slug is safe to use, modify if required
                var slug = seoMetaModel.Slug;
                SeoMeta savedSeoMeta = null;
                var index = 1;
                while ((savedSeoMeta = seoMetaService.FirstOrDefault(x => x.EntityName == typeof(T).Name && x.Slug == slug && x.Id != seoMeta.Id)) != null)
                {
                    slug = savedSeoMeta.Slug + (index++);
                }
                seoMeta.Slug = slug;
                seoMetaService.Update(seoMeta);
            }
        }
    }
}