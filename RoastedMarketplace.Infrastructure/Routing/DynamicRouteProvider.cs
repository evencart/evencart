using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Pages;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DynamicRouteProvider : IDynamicRouteProvider
    {
        private readonly UrlSettings _urlSettings;
        private readonly ISeoMetaService _seoMetaService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public DynamicRouteProvider(UrlSettings urlSettings, ISeoMetaService seoMetaService, ICategoryService categoryService, IProductService productService)
        {
            _urlSettings = urlSettings;
            _seoMetaService = seoMetaService;
            _categoryService = categoryService;
            _productService = productService;
        }

        public virtual DynamicRoute GetDynamicRoute(SeoMeta seoMeta, string requestPath)
        {
            var dynamicRoute = new DynamicRoute { Id = seoMeta.EntityId };
            switch (seoMeta.EntityName)
            {
                case "Product":
                    //check if it's a review page being requested?
                    if (requestPath.EndsWith(AppRouter.ReviewUrlSuffix))
                    {
                        dynamicRoute.Controller = "Reviews";
                        dynamicRoute.Action = "ReviewsList";
                        dynamicRoute.IdTypeName = "ProductId";
                    }
                    else
                    {
                        dynamicRoute.Controller = "Products";
                        dynamicRoute.Action = "Index";
                        dynamicRoute.IdTypeName = "Id";
                    }
                  
                    break;
                case "Category":
                    dynamicRoute.Controller = "Products";
                    dynamicRoute.Action = "ProductsList";
                    dynamicRoute.IdTypeName = "CategoryId";
                    break;
                default:
                    return null;
            }
            return dynamicRoute;
        }

        public virtual VirtualPathData GetVirtualPathData(IRouter router, VirtualPathContext context)
        {
            var categoryPath = "";
            var seName = "";
            var id = "0";
            var date = default(DateTime);

            if (context.Values.ContainsKey("id"))
                id = context.Values["id"].ToString();

            if (context.Values.ContainsKey("seName"))
                seName = context.Values["seName"].ToString();

            if (context.Values.ContainsKey("categoryPath"))
                categoryPath = context.Values["categoryPath"].ToString();

            if (context.Values.ContainsKey("date"))
                date = Convert.ToDateTime(context.Values["date"]);

            if (id.IsNullEmptyOrWhitespace() && seName.IsNullEmptyOrWhitespace())
            {
                throw new Exception("At least seName or id must be provided to generate the route");
            }
            var url = "";
            var entityName = "";
            switch (context.RouteName)
            {
                case RouteNames.SingleProduct:
                    url = _urlSettings.ProductUrlTemplate;
                    entityName = "Product";
                    break;
                case RouteNames.ReviewsList:
                    url = _urlSettings.ProductUrlTemplate + AppRouter.ReviewUrlSuffix;
                    entityName = "Product";
                    break;
                case RouteNames.ProductsPage:
                    url = _urlSettings.CategoryUrlTemplate;
                    entityName = "Category";
                    break;
            }

            
            if (id.IsNullEmptyOrWhitespace() && id != "0")
            {
                var seoMeta = _seoMetaService.FirstOrDefault(x => x.Slug == seName && x.EntityName == entityName);
                if (seoMeta == null)
                    return null;
                id = seoMeta.EntityId.ToString();
            }
            var idAsInt = 0;
            if (seName.IsNullEmptyOrWhitespace())
            {
                idAsInt = Convert.ToInt32(id);
                var seoMeta = _seoMetaService.FirstOrDefault(x => x.EntityId == idAsInt && x.EntityName == entityName);
                if (seoMeta == null)
                    return null;
                seName = seoMeta.Slug;
            }
            if (categoryPath.IsNullEmptyOrWhitespace() && url.Contains("{CategoryPath}"))
            {
                var allCategories = _categoryService.GetFullCategoryTree();
                switch (entityName)
                {
                    case "Category":
                        var primaryCategory = allCategories.FirstOrDefault(x => x.Id == idAsInt);
                        categoryPath = primaryCategory.GetCategoryPath(allCategories);
                        break;
                }
            }

            url = url.Replace("{Id}", id)
                .Replace("{SeName}", seName)
                .Replace("{CategoryPath}", categoryPath)
                .Replace("{Day}", date.Day.ToString())
                .Replace("{Month}", date.Month.ToString())
                .Replace("{Year}", date.Year.ToString())
                .Replace("//", "/");

            var vpd = new VirtualPathData(router, url);
            return vpd;
        }
    }
}