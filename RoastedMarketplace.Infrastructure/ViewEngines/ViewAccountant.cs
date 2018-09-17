using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Infrastructure.Providers;
using RoastedMarketplace.Infrastructure.ViewEngines.Expanders;
using RoastedMarketplace.Infrastructure.ViewEngines.Filters;
using RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class ViewAccountant : IViewAccountant
    {
        public const string ViewExtension = ".html";

        private readonly ConcurrentDictionary<string, CachedView> _parsedTemplateCache;

        private readonly ILocalFileProvider _localFileProvider;
        public ViewAccountant(ILocalFileProvider localFileProvider)
        {
            _localFileProvider = localFileProvider;
            _parsedTemplateCache = new ConcurrentDictionary<string, CachedView>();

            //set the file system
            Template.FileSystem = new AppThemeFileSystem(this);

            //register global objects
            GlobalObject.RegisterObject<NavigationObject>("navigation");

            //register filters
            //Template.RegisterFilter(typeof(TranslateFilter));

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
            return RenderView(viewContext.View.Path, viewContext.ViewData.Model);
        }

        public string RenderView(string viewPath, object parameters = null)
        {
            var template = GetView(viewPath).Template;
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

        public CachedView GetView(string viewPath)
        {
            Expander.ExpandView(viewPath, out string content);
            if (!_parsedTemplateCache.TryGetValue(viewPath, out CachedView cachedView) || cachedView.Raw != content)
            {
                if (cachedView == null)
                {
                    cachedView = new CachedView();
                    //save it for future
                    _parsedTemplateCache.TryAdd(viewPath, cachedView);
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
    }
}