using EvenCart.Data.Entity.Pages;

namespace EvenCart.Infrastructure.Routing.Extensions
{
    public static class RoutingExtensions
    {
        public static string GetParentPath(this ContentPage contentPage)
        {
            if (contentPage == null)
                return null;
            var c = contentPage.Parent;
            var path = "";
            while (c != null)
            {
                path = $"{c.SeoMeta.Slug}/" + path;
                c = c.Parent;
            }
            path = path.TrimEnd('/');
            return path;
        }
    }
}