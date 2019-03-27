using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Routing.Parsers;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Pages;
using RoastedMarketplace.Services.Products;
using RouteData = RoastedMarketplace.Core.Infrastructure.Routing.RouteData;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DynamicRouteProvider : IDynamicRouteProvider
    {
        private readonly ISeoMetaService _seoMetaService;
        private readonly ICategoryService _categoryService;
        private readonly IRouteTemplateParser _routeTemplateParser;

        public DynamicRouteProvider(ISeoMetaService seoMetaService, ICategoryService categoryService, IRouteTemplateParser routeTemplateParser)
        {
            _seoMetaService = seoMetaService;
            _categoryService = categoryService;
            _routeTemplateParser = routeTemplateParser;
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

            if (id.IsNullEmptyOrWhiteSpace() && seName.IsNullEmptyOrWhiteSpace())
            {
                throw new Exception("At least seName or id must be provided to generate the route");
            }
            var url = "";
            var entityName = "";
            var dynamicRoute = DynamicRoutes.FirstOrDefault(x => x.RouteName == context.RouteName);
            if (dynamicRoute == null)
                return null;

            url = dynamicRoute.Template;
            entityName = dynamicRoute.SeoEntityName;
            
            if (id.IsNullEmptyOrWhiteSpace() && id != "0")
            {
                var seoMeta = _seoMetaService.FirstOrDefault(x => x.Slug == seName && x.EntityName == entityName);
                if (seoMeta == null)
                    return null;
                id = seoMeta.EntityId.ToString();
            }
            var idAsInt = 0;
            if (seName.IsNullEmptyOrWhiteSpace())
            {
                idAsInt = Convert.ToInt32(id);
                var seoMeta = _seoMetaService.FirstOrDefault(x => x.EntityId == idAsInt && x.EntityName == entityName);
                if (seoMeta == null)
                    return null;
                seName = seoMeta.Slug;
            }
            if (categoryPath.IsNullEmptyOrWhiteSpace() && url.Contains("{CategoryPath}"))
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

        protected List<RouteData> DynamicRoutes;
        public virtual void RegisterDynamicRoute(RouteData routeData)
        {
            DynamicRoutes = DynamicRoutes ?? new List<RouteData>();
            DynamicRoutes.Add(routeData);
        }

        public virtual RouteData GetMatchingRoute(IRouter router, RouteContext routeContext)
        {
            if (DynamicRoutes == null)
                return null;
            var requestPath = routeContext.HttpContext.Request.Path.Value.TrimEnd('/');
            foreach (var dr in DynamicRoutes.OrderBy(x => x.Order))
            {
                var routeData = dr;
                var parsedTokens = _routeTemplateParser.ParsePathForTemplate(requestPath, routeData.Template);
                if (!parsedTokens.Any())
                    continue;
                parsedTokens.TryGetValue("SeName", out string seName);
                parsedTokens.TryGetValue("Id", out string id);
                if (seName.IsNullEmptyOrWhiteSpace() && id.IsNullEmptyOrWhiteSpace())
                    continue;

                var entityName = routeData.SeoEntityName;
                if (!id.IsNullEmptyOrWhiteSpace())
                {
                    var idAsInt = int.Parse(id);
                    var seoMeta =
                        _seoMetaService.FirstOrDefault(x => x.EntityName == entityName && x.Slug == seName &&
                                                            x.EntityId == idAsInt);
                    if (seoMeta == null)
                        continue;
                    //preserve seometa so we can serve it later
                    routeContext.HttpContext.SetRequestSeoMeta(seoMeta);
                    routeContext.RouteData.Values["id"] = id;
                    return routeData;
                }
                //if we are here, id was not passed, so we'll have to check all the available slugs for entitytype
                var matchingSeoMetas = _seoMetaService.Get(x => x.EntityName == entityName && x.Slug == seName).ToList();
                foreach (var seoMeta in matchingSeoMetas)
                {
                    //set id so we can generate correct reverse path for validation
                    routeContext.RouteData.Values["id"] = seoMeta.EntityId;
                    //check if the request path is correct 
                    var vpd = GetVirtualPathData(router,
                        new VirtualPathContext(ApplicationEngine.CurrentHttpContext, routeContext.RouteData.Values, routeContext.RouteData.Values, routeData.RouteName));
                    var url = vpd.VirtualPath;
                    if (requestPath != url)
                        continue;
                    routeContext.RouteData.Values["controller"] = routeData.ControllerName;
                    routeContext.RouteData.Values["action"] = routeData.ActionName;
                    routeContext.RouteData.Values[routeData.ParameterName] = seoMeta.EntityId;
                    //preserve seometa so we can serve it later
                    routeContext.HttpContext.SetRequestSeoMeta(seoMeta);

                    return routeData;
                }
            }
            return null;
        }
    }
}