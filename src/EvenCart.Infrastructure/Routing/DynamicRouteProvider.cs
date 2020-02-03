using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Pages;
using EvenCart.Services.Products;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Routing.Extensions;
using EvenCart.Infrastructure.Routing.Parsers;
using EvenCart.Services.Extensions;
using Microsoft.AspNetCore.Routing;
using RouteData = EvenCart.Core.Infrastructure.Routing.RouteData;

namespace EvenCart.Infrastructure.Routing
{
    public class DynamicRouteProvider : IDynamicRouteProvider
    {
        private readonly ISeoMetaService _seoMetaService;
        private readonly ICategoryService _categoryService;
        private readonly IRouteTemplateParser _routeTemplateParser;
        public readonly IContentPageService _contentPageService;
        public DynamicRouteProvider(ISeoMetaService seoMetaService, ICategoryService categoryService, IRouteTemplateParser routeTemplateParser, IContentPageService contentPageService)
        {
            _seoMetaService = seoMetaService;
            _categoryService = categoryService;
            _routeTemplateParser = routeTemplateParser;
            _contentPageService = contentPageService;
        }

        public virtual VirtualPathData GetVirtualPathData(IRouter router, VirtualPathContext context)
        {
            var categoryPath = "";
            var seName = "";
            var id = "0";
            var date = default(DateTime);
            var parentEntityPath = "";

            if (context.Values.ContainsKey("id"))
                id = context.Values["id"].ToString();

            if (context.Values.ContainsKey("seName"))
                seName = context.Values["seName"]?.ToString();

            if (context.Values.ContainsKey("categoryPath"))
                categoryPath = context.Values["categoryPath"]?.ToString();

            if (context.Values.ContainsKey("parentEntityPath"))
                parentEntityPath = context.Values["parentEntityPath"]?.ToString();

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

            if (idAsInt == 0)
                int.TryParse(id, out idAsInt);

            if (categoryPath.IsNullEmptyOrWhiteSpace() && url.Contains("{CategoryPath}"))
            {
                switch (entityName)
                {
                    case nameof(Category):
                        var allCategories = _categoryService.GetFullCategoryTree();
                        var primaryCategory = allCategories.FirstOrDefault(x => x.Id == idAsInt);
                        categoryPath = primaryCategory.GetCategoryPath();
                        break;
                }
            }

            if (parentEntityPath.IsNullEmptyOrWhiteSpace() && url.Contains("{ParentEntityPath}"))
            {
                switch (entityName)
                {
                    case nameof(ContentPage):
                        var contentPage = _contentPageService.GetWithTree(idAsInt);
                        parentEntityPath = contentPage.GetParentPath();
                        break;
                }
            }
            url = url.Replace("{Id}", id)
                .Replace("{SeName}", seName)
                .Replace("{CategoryPath}", categoryPath)
                .Replace("{ParentEntityPath}", parentEntityPath)
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
                    routeContext.RouteData.Values["controller"] = routeData.ControllerName;
                    routeContext.RouteData.Values["action"] = routeData.ActionName;
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