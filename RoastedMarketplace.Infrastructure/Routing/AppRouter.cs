using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure.Routing.Parsers;
using RoastedMarketplace.Services.Pages;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class AppRouter : IRouter
    {
        private readonly IRouter _defaultRouter;
        private readonly IDynamicRouteProvider _dynamicRouteProvider;
        private readonly IList<string> _templatesToCheck;
        public AppRouter(IRouter defaultRouter)
        {
            _defaultRouter = defaultRouter;
            _dynamicRouteProvider = DependencyResolver.Resolve<IDynamicRouteProvider>();
            //the url templates to check
            var urlSettings = DependencyResolver.Resolve<UrlSettings>();
            _templatesToCheck = new List<string>()
            {
                urlSettings.ProductUrlTemplate,
                urlSettings.CategoryUrlTemplate,
            };
        }

        public async Task RouteAsync(RouteContext context)
        {
            var seoMetaService = DependencyResolver.Resolve<ISeoMetaService>();
            var routeTemplateParser = DependencyResolver.Resolve<IRouteTemplateParser>();
            var requestPath = context.HttpContext.Request.Path.Value.TrimEnd('/');
            var actionSelector = DependencyResolver.Resolve<IActionSelector>();
         
            foreach (var template in _templatesToCheck)
            {
                var parsedTokens = routeTemplateParser.ParsePathForTemplate(requestPath, template);
                if (!parsedTokens.Any())
                {
                    continue;
                }
                parsedTokens.TryGetValue("SeName", out string seName); ;
                //find the matching type
                var seoMetasWithMatchingSlugs = seoMetaService.Search(seName);
                foreach (var seoMeta in seoMetasWithMatchingSlugs)
                {
                    string routeName = null;
                    switch (seoMeta.EntityName)
                    {
                        case nameof(Category):
                            routeName = RouteNames.ProductsPage;
                            break;
                        case nameof(Product):
                            routeName = RouteNames.SingleProduct;
                            break;
                        default:
                            continue;
                    }
                    //we need to check if person is hitting the right url so we are not just checking the slug but entire path
                    //todo: check if there is a better way, I don't know if this is the best
                    context.RouteData.Values["id"] = seoMeta.EntityId;
                    //recreate route to make sure that we are hitting the right page
                    var vpd = _dynamicRouteProvider.GetVirtualPathData(this,
                        new VirtualPathContext(ApplicationEngine.CurrentHttpContext, context.RouteData.Values,
                            context.RouteData.Values, routeName));
                    var url = vpd.VirtualPath;
                    if (requestPath != url)
                        continue;
                    var dynamicRoute = _dynamicRouteProvider.GetDynamicRoute(seoMeta);
                    if (dynamicRoute == null)
                        continue;
                    context.RouteData.Values["controller"] = dynamicRoute.Controller;
                    context.RouteData.Values["action"] = dynamicRoute.Action;
                    context.RouteData.Values["area"] = "";
                    context.RouteData.Values[dynamicRoute.IdTypeName] = dynamicRoute.Id;
                    var candidates = actionSelector.SelectCandidates(context);
                    var best = actionSelector.SelectBestCandidate(context, candidates);
                    await _defaultRouter.RouteAsync(context);
                    return;
                }
            }
            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return _dynamicRouteProvider.GetVirtualPathData(this, context);
        }
    }
}