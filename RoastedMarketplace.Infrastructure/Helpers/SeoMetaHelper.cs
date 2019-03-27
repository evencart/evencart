using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Routing;

namespace RoastedMarketplace.Infrastructure.Helpers
{
    public static class SeoMetaHelper
    {
        public static string GetUrl(SeoMeta seoMeta, string categoryPath = null)
        {
            switch(seoMeta.EntityName)
            {
                case nameof(Product):
                    return ApplicationEngine.RouteUrl(RouteNames.SingleProduct, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
                case nameof(Category):
                    return  ApplicationEngine.RouteUrl(RouteNames.ProductsPage, new { seName = seoMeta.Slug, id = seoMeta.EntityId, categoryPath = categoryPath });
                case nameof(ContentPage):
                    return ApplicationEngine.RouteUrl(RouteNames.SinglePage, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
            }

            return null;
        }

        public static void SetSeoData(string title, string description = null, string keywords = null)
        {
            var seoMeta = ApplicationEngine.CurrentHttpContext.GetRequestSeoMeta();
            if (seoMeta == null)
            {
                seoMeta = new SeoMeta();
                ApplicationEngine.CurrentHttpContext.SetRequestSeoMeta(seoMeta);
            }

            if (seoMeta.PageTitle.IsNullEmptyOrWhiteSpace())
                seoMeta.PageTitle = title;
            if (seoMeta.MetaKeywords.IsNullEmptyOrWhiteSpace())
                seoMeta.MetaKeywords = keywords;
            if (seoMeta.MetaDescription.IsNullEmptyOrWhiteSpace())
                seoMeta.MetaDescription = description;
        }
    }
}