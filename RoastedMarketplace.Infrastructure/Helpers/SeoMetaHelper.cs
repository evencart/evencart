using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Shop;
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
    }
}