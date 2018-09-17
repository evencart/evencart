using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public interface IViewAccountant
    {
        string GetThemeViewPath(string viewName);

        IList<string> GetSearchLocations();

        string RenderView(ViewContext viewContext);

        string RenderView(string viewName, object parameters = null);

        CachedView GetView(string viewName);

        string GetLayoutPath(string layoutName);

    }
}