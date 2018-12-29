using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Providers;
using RoastedMarketplace.Infrastructure.ViewEngines;
using RoastedMarketplace.Infrastructure.ViewEngines.Expanders;

namespace RoastedMarketplace.Infrastructure.Mvc.Components
{
    public class ViewComponentManager : IViewComponentManager
    {
        private readonly IViewComponentSelector _viewComponentSelector;
        private readonly IViewComponentHelper _viewComponentHelper;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IViewAccountant _viewAccountant;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly ILocalFileProvider _localFileProvider;

        public ViewComponentManager(IViewComponentSelector viewComponentSelector, IViewComponentHelper viewComponentHelper, IActionContextAccessor actionContextAccessor, IViewAccountant viewAccountant, ITempDataProvider tempDataProvider, ILocalFileProvider localFileProvider)
        {
            _viewComponentSelector = viewComponentSelector;
            _viewComponentHelper = viewComponentHelper;
            _actionContextAccessor = actionContextAccessor;
            _viewAccountant = viewAccountant;
            _tempDataProvider = tempDataProvider;
            _localFileProvider = localFileProvider;
        }

        [Obsolete]
        public string GetViewComponentContent(string componentName, object parameters = null)
        {
            var defaultViewComponentHelper = _viewComponentHelper as DefaultViewComponentHelper;
            if (defaultViewComponentHelper == null)
                return string.Empty;
            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {
                Model = parameters
            };
            var viewContext = new ViewContext(_actionContextAccessor.ActionContext,
                new RoastedLiquidView(componentName, componentName, _viewAccountant), viewDictionary,
                new TempDataDictionary(_actionContextAccessor.ActionContext.HttpContext, _tempDataProvider),
                new StringWriter(),
                new HtmlHelperOptions());

            defaultViewComponentHelper.Contextualize(viewContext);
            var content = defaultViewComponentHelper.InvokeAsync(componentName, parameters).Result;
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        public void InvokeViewComponent(string componentName, object parameters, out string viewHtml, out object model, out string viewPath, bool onlyModel = false)
        {
            viewHtml = null;
            model = null;
            viewPath = null;
            var component = _viewComponentSelector.SelectComponent(componentName);
            if (component == null)
                return;
            if (!onlyModel)
            {
                var componentPath = $"Components/{component.FullName}/Default";
                viewPath = _viewAccountant.GetThemeViewPath(componentPath);
                if (viewPath.IsNullEmptyOrWhitespace())
                    return;
                viewHtml = ReadFile.From(viewPath).Content;
            }
            var instance = DependencyResolver.ResolveOptional(component.TypeInfo.AsType(), true);
            var viewComponentResult = (ViewViewComponentResult)component.MethodInfo.Invoke(instance, new[] { parameters });
            model = viewComponentResult.ViewData.Model;
        }
    }
}