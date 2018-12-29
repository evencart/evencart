using System;

namespace RoastedMarketplace.Infrastructure.Mvc.Components
{
    public interface IViewComponentManager
    {
        [Obsolete]
        string GetViewComponentContent(string componentName, object parameters = null);

        void InvokeViewComponent(string componentName, object parameters, out string viewHtml, out object model, out string viewPath, bool onlyModel = false);
    }
}