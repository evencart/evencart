using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Data.Helpers;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Infrastructure.Helpers
{
    public static class SeoMetaHelper
    {
        public static string GetUrl(SeoMeta seoMeta)
        {
            switch(seoMeta.EntityName)
            {
                case nameof(Product):
                    return ApplicationEngine.RouteUrl(RouteNames.SingleProduct, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
                case nameof(Category):
                    return  ApplicationEngine.RouteUrl(RouteNames.ProductsPage, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
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
                seoMeta.PageTitle = HtmlUtility.StripHtml(title);
            if (seoMeta.MetaKeywords.IsNullEmptyOrWhiteSpace())
                seoMeta.MetaKeywords = HtmlUtility.StripHtml(keywords);
            if (seoMeta.MetaDescription.IsNullEmptyOrWhiteSpace())
                seoMeta.MetaDescription = HtmlUtility.StripHtml(description);
        }
    }
}