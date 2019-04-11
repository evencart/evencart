using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.Mvc.UI.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static void RegisterResource(this HtmlHelper htmlHelper, string resourceName, string resourcePath, ResourceRegistrationType registrationType)
        {
            DependencyResolver.Resolve<IPageGenerator>().RegisterResource(resourceName, resourcePath, registrationType);
        }

        public static void EnqueueStyles(this HtmlHelper htmlHelper, string[] resourceNames, string[] requiredResourceNames = null, ResourcePlacementType placementType = ResourcePlacementType.HeadTag)
        {
            DependencyResolver.Resolve<IPageGenerator>().EnqueueStyles(resourceNames, requiredResourceNames, placementType);
        }

        public static void EnqueueScripts(this HtmlHelper htmlHelper, string[] resourceNames, string[] requiredResourceNames = null, ResourcePlacementType placementType = ResourcePlacementType.BeforeEndBodyTag)
        {
            DependencyResolver.Resolve<IPageGenerator>().EnqueueScripts(resourceNames, requiredResourceNames, placementType);
        }

        public static HtmlString RenderStyles(this HtmlHelper htmlHelper, ResourcePlacementType placementType, bool includeAsBundle = false)
        {
            var pageGenerator = DependencyResolver.Resolve<IPageGenerator>();
            return new HtmlString(pageGenerator.RenderStyles(placementType, includeAsBundle));
        }

        public static HtmlString RenderScripts(this HtmlHelper htmlHelper, ResourcePlacementType placementType, bool includeAsBundle = false)
        {
            var pageGenerator = DependencyResolver.Resolve<IPageGenerator>();
            return new HtmlString(pageGenerator.RenderScripts(placementType, includeAsBundle));
        }

        public static HtmlString DisplayWidgets(this HtmlHelper htmlHelper, string widgetLocation)
        {
            return HtmlString.Empty;
            
        }
    }
}