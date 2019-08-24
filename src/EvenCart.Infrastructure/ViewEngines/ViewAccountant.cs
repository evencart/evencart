using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Plugins;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Bundle;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Theming;
using EvenCart.Infrastructure.ViewEngines.Expanders;
using EvenCart.Infrastructure.ViewEngines.Filters;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects;
using EvenCart.Infrastructure.ViewEngines.NamingConventions;
using EvenCart.Infrastructure.ViewEngines.Tags;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Infrastructure.ViewEngines
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
        private readonly IAntiforgery _antiforgery;
        private readonly IMinifier _minifier;
        public ViewAccountant(ILocalFileProvider localFileProvider, IActionContextAccessor actionContextAccessor, IThemeProvider themeProvider, IPluginLoader pluginLoader, IHtmlProcessor htmlProcessor, IAntiforgery antiforgery, IMinifier minifier)
        {
            _localFileProvider = localFileProvider;
            _actionContextAccessor = actionContextAccessor;
            _themeProvider = themeProvider;
            _pluginLoader = pluginLoader;
            _htmlProcessor = htmlProcessor;
            _antiforgery = antiforgery;
            _minifier = minifier;
            _parsedTemplateCache = new ConcurrentDictionary<CachedViewKey, CachedView>();

            //set the file system
            Template.FileSystem = new AppThemeFileSystem(this);
            //naming convention camelCaseConvention
            Template.NamingConvention = new CamelCaseNamingConvention();
            //register additional types
            Template.RegisterSafeType(typeof(SelectListItem), new[] { "Text", "Value", "Selected" });

            //register all the enums
            var enumTypes = TypeFinder.EnumTypes();
            foreach (var enumType in enumTypes)
            {
                Template.RegisterSafeType(enumType, x => x.ToString());
            }

            Template.RegisterFilter(typeof(TextFilters));
            Template.RegisterTag<Increment>("increment");
            //register global objects
            GlobalObject.RegisterObject<StoreObject>(GlobalObjectKeys.Store);
            GlobalObject.RegisterObject<CartObject>(GlobalObjectKeys.Cart);
            GlobalObject.RegisterObject<BreadcrumbObject>(GlobalObjectKeys.Breadcrumb);
            GlobalObject.RegisterObject<NavigationObject>(GlobalObjectKeys.Navigation);
            GlobalObject.RegisterObject<CurrentUserObject>(GlobalObjectKeys.CurrentUser);
        }

        private string ValidateViewName(string viewName)
        {
            if (!viewName.EndsWith(ViewExtension))
                viewName = viewName + ViewExtension;
            viewName = viewName.Replace("/", "\\");
            return viewName;
        }

        public string GetThemeViewPath(string viewName, bool ignoreAdminViews = false)
        {
            viewName = ValidateViewName(viewName);
            return GetViewLocations(ignoreAdminViews)
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
            var resultHash = GetTemplateHash(parameters);
            var raw = template.Render(resultHash);
            return GetProcessedHtml(raw);
        }

        public string RenderView(string viewName, string htmlContent, object parameters = null)
        {
            try
            {
                var template = Template.Parse(htmlContent);
                var resultHash = GetTemplateHash(parameters);
                var raw = template.Render(resultHash);
                return GetProcessedHtml(raw);
            }
            catch (Exception ex)
            {
                var newEx = new Exception($"Error occured while parsing template {viewName}", ex);
                throw newEx;
            }
        }

        public CachedView GetView(string viewPath, string requestedPath, string area, object parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
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
                try
                {
                    var template = Template.Parse(content);
                    cachedView.Template = template;
                    cachedView.Raw = content;
                }
                catch (Exception ex)
                {
                    var newEx = new Exception($"Error occured while parsing file. File: {viewPath}", ex);
                    throw newEx;
                }

            }
            return cachedView;
        }

        public string GetLayoutPath(string layoutName)
        {
            var rootPath = ServerHelper.MapPath("~/");
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
                if (!dictionary.ContainsKey(gt.Key))
                    dictionary.Add(gt.Key.ToLower(), null);

                dictionary[gt.Key.ToLower()] = gt.ToDictionary(x => x.Key.Url, x =>
                {
                    if (splitted)
                    {
                        return x.Value.ToSplited();
                    }
                    return (object)x.Value.Raw;
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
                if (themeViewPath.IsNullEmptyOrWhiteSpace())
                    continue;
                GetView(themeViewPath, viewPath, area);
            }
            return GetCompiledViews(splitted, area);
        }

        private IList<string> _viewLocations;
        private IList<string> _adminViewLocations;
        private IList<string> GetViewLocations(bool ignoreAdminViews = false)
        {
            var rootPath = ServerHelper.MapPath("~/");
            var plugins = _pluginLoader.GetAvailablePlugins();
            if (!ignoreAdminViews && ApplicationEngine.IsAdmin())
            {
                if (_adminViewLocations != null)
                    return _adminViewLocations;
                _adminViewLocations = new List<string>();

                _adminViewLocations.Add(_localFileProvider.CombinePaths(rootPath, "Areas", "Administration", "Views"));

                //then the plugin locations
                foreach (var plugin in plugins)
                {
                    _adminViewLocations.Add(_localFileProvider.CombinePaths(plugin.PluginDirectory, "Views", "Administration"));
                }
                return _adminViewLocations;
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

        private Hash GetTemplateHash(object parameters = null)
        {
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

            var isAdmin = ApplicationEngine.IsAdmin();
            //add global objects
            foreach (var globalObjectKp in GlobalObject.RegisteredObjects)
            {
                if (isAdmin && !globalObjectKp.Value.RenderInAdmin)
                    continue;
                if (!isAdmin && !globalObjectKp.Value.RenderInPublic)
                    continue;
                resultHash.Add(globalObjectKp.Key, globalObjectKp.Value.GetObject());
            }

            if (DatabaseManager.IsDatabaseInstalled())
            {
                var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
                //if there is an seometa, do that as well
                var seoMeta = ApplicationEngine.CurrentHttpContext.GetRequestSeoMeta();
                resultHash.Add("pageTitle", seoMeta?.PageTitle ?? generalSettings.DefaultPageTitle);
                resultHash.Add("metaKeywords", seoMeta?.MetaKeywords ?? generalSettings.DefaultMetaKeywords);
                resultHash.Add("metaDescription", seoMeta?.MetaDescription ?? generalSettings.DefaultMetaDescription);
            }
         

            return resultHash;
        }

        private string GetProcessedHtml(string html)
        {
            if (DatabaseManager.IsDatabaseInstalled())
            {
                var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
                if (!generalSettings.EnableHtmlMinification)
                    return html;
                var minified = _minifier.MinifyHtml(html);
                return minified;
            }

            return html;
        }
    }
}