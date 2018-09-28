using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using DotLiquid.NamingConventions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure.Providers;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.ViewEngines.Expanders;
using RoastedMarketplace.Infrastructure.ViewEngines.Filters;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects;
using RoastedMarketplace.Infrastructure.ViewEngines.NamingConventions;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class ViewAccountant : IViewAccountant
    {
        public const string ViewExtension = ".html";

        private readonly ConcurrentDictionary<CachedViewKey, CachedView> _parsedTemplateCache;

        private readonly ILocalFileProvider _localFileProvider;
        private readonly IActionContextAccessor _actionContextAccessor;
        public ViewAccountant(ILocalFileProvider localFileProvider, IActionContextAccessor actionContextAccessor)
        {
            _localFileProvider = localFileProvider;
            _actionContextAccessor = actionContextAccessor;
            _parsedTemplateCache = new ConcurrentDictionary<CachedViewKey, CachedView>();

            //set the file system
            Template.FileSystem = new AppThemeFileSystem(this);
            //naming convention camelCaseConvention
            Template.NamingConvention = new CamelCaseNamingConvention();
            //register global objects
            GlobalObject.RegisterObject<PrimaryNavigationObject>("primary_navigation");
            GlobalObject.RegisterObject<SecondaryNavigationObject>("secondary_navigation");
            GlobalObject.RegisterObject<SelectOptionsObject>("selectOptions");
            //register additional types
            Template.RegisterSafeType(typeof(SelectListItem), new string[] { "Text", "Value", "Selected"});
        }

        private string ValidateViewName(string viewName)
        {
            if (!viewName.EndsWith(ViewExtension))
                viewName = viewName + ViewExtension;
            viewName = viewName.Replace("/", "\\");
            return viewName;
        }

        public string GetThemeViewPath(string viewName)
        {
            viewName = ValidateViewName(viewName);
            return GetViewLocations()
                .Select(x => _localFileProvider.CombinePaths(x, viewName))
                .FirstOrDefault(x => _localFileProvider.FileExists(x));
        }

        public IList<string> GetSearchLocations()
        {
            return GetViewLocations();
        }

        public string RenderView(ViewContext viewContext)
        {
            var originalPath = viewContext.TempData["requested_path"].ToString();
            return RenderView(viewContext.View.Path, originalPath, viewContext.ViewData.Model);
        }

        public string RenderView(string viewPath, string originalViewPath, object parameters = null)
        {
            var template = GetView(viewPath, originalViewPath).Template;
            Hash resultHash = null;
            if (parameters != null)
            {
                if (parameters is IDictionary<string, object>)
                    resultHash = Hash.FromDictionary((IDictionary<string, object>)parameters);
                else if (parameters.GetType() == typeof(Hash))
                    resultHash = (Hash)parameters;
                else
                    resultHash = Hash.FromAnonymousObject(parameters, true);
            }
            else
                resultHash = new Hash();

            //add global objects
            foreach (var globalObjectKp in GlobalObject.RegisteredObjects)
            {
                resultHash.Add(globalObjectKp.Key, globalObjectKp.Value.GetObject());
            }
            return template.Render(resultHash);
        }

        public CachedView GetView(string viewPath, string requestedPath)
        {
            Expander.ExpandView(viewPath, out string content);
            var cacheKey = GetCachedViewKey(requestedPath);
            if (!_parsedTemplateCache.TryGetValue(cacheKey, out CachedView cachedView) || cachedView.Raw != content)
            {
                if (cachedView == null)
                {
                    cachedView = new CachedView();
                    //save it for future
                    _parsedTemplateCache.TryAdd(cacheKey, cachedView);
                }
                //run filters for the view
                content = Filter.RunAll(content);
                var template = Template.Parse(content);
                cachedView.Template = template;
                cachedView.Raw = content;
            }
            return cachedView;
        }

        public string GetLayoutPath(string layoutName)
        {
            var rootPath = ApplicationEngine.MapPath("~/");
            layoutName = ValidateViewName(layoutName);
            var isAdmin = ApplicationEngine.IsAdmin();
            var pathBuilder = _localFileProvider.CombinePaths(rootPath);
            if (isAdmin)
                pathBuilder = _localFileProvider.CombinePaths(pathBuilder, "Areas", "Administration");

            var layoutPath = _localFileProvider.CombinePaths(pathBuilder, "Views", "Layout", layoutName);
            if (_localFileProvider.FileExists(layoutPath))
                return layoutPath;
            return string.Empty;
        }

        public Dictionary<string, object> GetCompiledViews()
        {
            var dictionary = new Dictionary<string, object>();
            var groupedTemplates = _parsedTemplateCache.GroupBy(x => x.Key.Context);
            foreach (var gt in groupedTemplates)
            {
                if(!dictionary.ContainsKey(gt.Key))
                    dictionary.Add(gt.Key.ToLower(), null);

                dictionary[gt.Key.ToLower()] = gt.ToDictionary(x => x.Key.Url, x => x.Value.Raw);
            }
            return dictionary;
        }

        public Dictionary<string, object> CompileAllViews(string controller = null)
        {
            //get all the routes
            var routes = RouteFinder.GetAllRoutes(controller);
            //conventionally we use the same name for views as our action name
            const string pathFormat = "{0}/{1}";
            foreach (var route in routes)
            {
                var viewPath = string.Format(pathFormat, route.Controller, route.Action);
                var themeViewPath = GetThemeViewPath(viewPath);
                if (themeViewPath.IsNullEmptyOrWhitespace())
                    continue;
                GetView(themeViewPath, viewPath);
            }
            return GetCompiledViews();
        }

        private IList<string> _viewLocations;
        private IList<string> _adminViewLocations;
        private IList<string> GetViewLocations()
        {
            var rootPath = ApplicationEngine.MapPath("~/");
            if (ApplicationEngine.IsAdmin())
            {
                return _adminViewLocations ?? (_adminViewLocations = new List<string>()
                {
                    _localFileProvider.CombinePaths(rootPath, "Areas", "Administration", "Views"),
                });
            }
            var themePath = ApplicationEngine.ActiveTheme.GetThemePath();
            return _viewLocations ?? (_viewLocations = new List<string>()
            {
                //first search for file in the active theme
                _localFileProvider.CombinePaths(themePath, "Views"),
                _localFileProvider.CombinePaths(rootPath, "Views"),
            });
        }

        private CachedViewKey GetCachedViewKey(string viewPath)
        {
            return CachedViewKey.Get(viewPath, ApplicationEngine.CurrentLanguageCultureCode);
        }
    }
}