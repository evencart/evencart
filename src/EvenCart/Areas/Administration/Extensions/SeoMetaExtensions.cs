using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Extensions;
using EvenCart.Services.Pages;

namespace EvenCart.Areas.Administration.Extensions
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
                if (!seoMetaModel.Slug.IsNullEmptyOrWhiteSpace())
                {
                    //check if slug is safe to use, modify if required
                    var slug = seoMetaModel.Slug;
                    SeoMeta savedSeoMeta = null;
                    var index = 1;
                    while ((savedSeoMeta = seoMetaService.FirstOrDefault(x => x.EntityName == typeof(T).Name && x.Slug == slug && x.Id != seoMeta.Id)) != null)
                    {
                        slug = savedSeoMeta.Slug + (index++);
                    }
                    seoMeta.Slug = slug;
                }
                seoMetaService.Update(seoMeta);
            }
        }
    }
}