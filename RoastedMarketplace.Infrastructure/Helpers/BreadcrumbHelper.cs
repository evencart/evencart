using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure.Mvc.Breadcrumbs;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Extensions;

namespace RoastedMarketplace.Infrastructure.Helpers
{
    public static class BreadcrumbHelper
    {
        public static void SetCategoryBreadcrumb(Category category, IList<Category> allCategories)
        {
            var breadcrumbNodes = new List<BreadcrumbNode>();
            while (category != null)
            {
                breadcrumbNodes.Insert(0, new BreadcrumbNode()
                {
                    DisplayText = category.Name,
                    Description = category.Description,
                    Url = category.SeoMeta != null ? ApplicationEngine.RouteUrl(RouteNames.ProductsPage, new
                    {
                        seName = category.SeoMeta.Slug,
                        categoryPath = category.GetCategoryPath(allCategories)
                    }) : ""
                });
                category = category.ParentCategory;
            }

            foreach (var node in breadcrumbNodes)
                ApplicationEngine.CurrentHttpContext.AppendToBreadcrumb(node);
        }
    }
}