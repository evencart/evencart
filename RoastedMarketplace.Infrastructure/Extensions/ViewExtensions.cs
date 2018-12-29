using DotLiquid;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.ViewEngines;

namespace RoastedMarketplace.Infrastructure.Extensions
{
    public static class ViewExtensions
    {
        public static bool IsLayoutTemplate(this Template template)
        {
            return false;
        }

        public static bool HasLayout(this Template template)
        {
            return false;
        }

        public static ViewSplited ToSplited(this CachedView cachedView)
        {
            if (cachedView == null)
                return null;
            var htmlProcessor = DependencyResolver.Resolve<IHtmlProcessor>();
            var body = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/body");
            var title = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head/title", true);
            var head = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head");
            var description = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head/meta[@name=""description""]");
            return new ViewSplited() {
                BodyHtml = body,
                Description = description,
                HeadHtml = head,
                Title = title
            };
        }
    }
}