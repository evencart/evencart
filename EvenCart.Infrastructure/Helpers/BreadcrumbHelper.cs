using System.Collections.Generic;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Breadcrumbs;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;

namespace EvenCart.Infrastructure.Helpers
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
                    }, true) : ""
                });
                category = category.ParentCategory;
            }

            foreach (var node in breadcrumbNodes)
                ApplicationEngine.CurrentHttpContext.AppendToBreadcrumb(node);
        }
    }
}