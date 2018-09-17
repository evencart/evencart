using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class DefaultAppViewEngine : IAppViewEngine
    {
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            return GetView(viewName, isMainPage);
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return GetView(viewPath, isMainPage);
        }

        private ViewEngineResult GetView(string viewName, bool isMainPage)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var viewFilePath = viewAccountant.GetThemeViewPath(viewName);
            if (!viewFilePath.IsNullEmptyOrWhitespace())
            {
                return ViewEngineResult.Found(viewFilePath, new RoastedLiquidView(viewFilePath, viewAccountant));
            }
            return ViewEngineResult.NotFound(viewName, viewAccountant.GetSearchLocations());
        }
    }
}