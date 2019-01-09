using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure.Providers;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Theming;
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
        private readonly IThemeProvider _themeProvider;
        private readonly IPluginLoader _pluginLoader;
        private readonly IHtmlProcessor _htmlProcessor;

        public ViewAccountant(ILocalFileProvider localFileProvider, IActionContextAccessor actionContextAccessor, IThemeProvider themeProvider, IPluginLoader pluginLoader, IHtmlProcessor htmlProcessor)
        {
            _localFileProvider = localFileProvider;
            _actionContextAccessor = actionContextAccessor;
            _themeProvider = themeProvider;
            _pluginLoader = pluginLoader;
            _htmlProcessor = htmlProcessor;
            _parsedTemplateCache = new ConcurrentDictionary<CachedViewKey, CachedView>();

            //set the file system
            Template.FileSystem = new AppThemeFileSystem(this);
            //naming convention camelCaseConvention
            Template.NamingConvention = new CamelCaseNamingConvention();
            //register additional types
            Template.RegisterSafeType(typeof(SelectListItem), new[] { "Text", "Value", "Selected"});

            //register all the enums
            var enumTypes = TypeFinder.EnumTypes();
            foreach (var enumType in enumTypes)
            {
                Template.RegisterSafeType(enumType,x => x.ToString());
            }

            Template.RegisterFilter(typeof(TextFilters));

            //register global objects
            GlobalObject.RegisterObject<StoreObject>("store");
            GlobalObject.RegisterObject<CartObject>("cart");
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
            var area = viewContext.RouteData.Values["area"]?.ToString();
            return RenderView(viewContext.View.Path, originalPath, area, viewContext.ViewData.Model);
        }

        public string RenderView(string viewPath, string originalViewPath, string area, object parameters = null)
        {
            var template = GetView(viewPath, originalViewPath, area, parameters).Template;
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

        public CachedView GetView(string viewPath, string requestedPath, string area, object parameters = null)
        {
            Expander.ExpandView(viewPath, parameters, out string content);
            var cacheKey = GetCachedViewKey(requestedPath, area);
            if (!_parsedTemplateCache.TryGetValue(cacheKey, out CachedView cachedView) || cachedView.Raw != content)
            {
                if (cachedView == null)
                {
                    cachedView = new CachedView();
                    //save it for future
                    _parsedTemplateCache.TryAdd(cacheKey, cachedView);
                }
                //run filters for the view
                //content = Filter.RunAll(content); //we've now moved to dotliquid filter method. That is better and more flexible
                try
                {
                    var template = Template.Parse(content);
                    cachedView.Template = template;
                    cachedView.Raw = content;
                }
                catch(Exception ex)
                {
                    var newEx = new Exception($"Error occured while parsing file. File: {viewPath}", ex);
                    throw newEx;
                }
              
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
            else
            {
                var themePath = _themeProvider.GetThemePath(ApplicationEngine.ActiveTheme.Name);
                pathBuilder = themePath;
            }
            var layoutPath = _localFileProvider.CombinePaths(pathBuilder, "Views", "Layout", layoutName);
            if (_localFileProvider.FileExists(layoutPath))
                return layoutPath;

            //if we are here and accessing public site, the layout was not found, so look for layout on the root
            if (!isAdmin)
            {
                layoutPath = _localFileProvider.CombinePaths(rootPath, "Views", "Layout", layoutName);
                if (_localFileProvider.FileExists(layoutPath))
                    return layoutPath;
            }
            return string.Empty;
        }

        public Dictionary<string, object> GetCompiledViews(bool splitted = false, string area = null)
        {
            var dictionary = new Dictionary<string, object>();
            var groupedTemplates = _parsedTemplateCache.GroupBy(x => x.Key.Context);
            foreach (var gt in groupedTemplates)
            {
                if(!dictionary.ContainsKey(gt.Key))
                    dictionary.Add(gt.Key.ToLower(), null);

                dictionary[gt.Key.ToLower()] = gt.ToDictionary(x => x.Key.Url, x =>
                {
                    if (splitted)
                    {
                        return x.Value.ToSplited();
                    }
                    return (object) x.Value.Raw;
                });
            }
            return dictionary;
        }

        public Dictionary<string, object> CompileAllViews(string controller = null, string area = null, bool splitted = false)
        {
            //get all the routes
            var routes = RouteFinder.GetAllRoutes(controller, area);
            //conventionally we use the same name for views as our action name
            const string pathFormat = "{0}/{1}";
            foreach (var route in routes)
            {
                var viewPath = string.Format(pathFormat, route.Controller, route.Action);
                var themeViewPath = GetThemeViewPath(viewPath);
                if (themeViewPath.IsNullEmptyOrWhitespace())
                    continue;
                GetView(themeViewPath, viewPath, area);
            }
            return GetCompiledViews(splitted, area);
        }

        private IList<string> _viewLocations;
        private IList<string> _adminViewLocations;
        private IList<string> GetViewLocations()
        {
            var rootPath = ApplicationEngine.MapPath("~/");
            var plugins = _pluginLoader.GetAvailablePlugins();
            if (ApplicationEngine.IsAdmin())
            {
                return _adminViewLocations ?? (_adminViewLocations = new List<string>()
                {
                    _localFileProvider.CombinePaths(rootPath, "Areas", "Administration", "Views"),
                });
            }
            var themePath = _themeProvider.GetThemePath(ApplicationEngine.ActiveTheme.Name);
            if (_viewLocations != null)
                return _viewLocations;

            _viewLocations = new List<string>();
            //first search for file in the active theme
            _viewLocations.Add(_localFileProvider.CombinePaths(themePath, "Views"));

            //then the plugin locations
            foreach (var plugin in plugins)
            {
                //in theme for each plugin
                _viewLocations.Add(_localFileProvider.CombinePaths(themePath, "Views", plugin.SystemName));

                _viewLocations.Add(_localFileProvider.CombinePaths(plugin.PluginDirectory, "Views"));
            }
            //finally the root
            _viewLocations.Add(_localFileProvider.CombinePaths(rootPath, "Views"));
            
            return _viewLocations;
        }

        private CachedViewKey GetCachedViewKey(string viewPath, string area)
        {
            return CachedViewKey.Get(viewPath, ApplicationEngine.CurrentLanguageCultureCode, area);
        }
    }
}