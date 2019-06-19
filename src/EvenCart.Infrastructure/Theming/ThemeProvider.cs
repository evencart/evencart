using System.Collections.Generic;
using EvenCart.Core;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Services.Serializers;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeProvider : IThemeProvider
    {
        private readonly ILocalFileProvider _localFileProvider;
        private readonly IDataSerializer _dataSerializer;
        public ThemeProvider(ILocalFileProvider localFileProvider, IDataSerializer dataSerializer)
        {
            _localFileProvider = localFileProvider;
            _dataSerializer = dataSerializer;
        }

        private ThemeInfo _cachedThemeInfo = null;
        public ThemeInfo GetActiveTheme()
        {
            if (_cachedThemeInfo != null)
                return _cachedThemeInfo;
            var themeName = "Default";
            var themePath = GetThemePath(themeName);
            var themeConfigPath = _localFileProvider.CombinePaths(themePath, "config.json");
            var configJson = _localFileProvider.ReadAllText(themeConfigPath);
            _cachedThemeInfo = _dataSerializer.DeserializeAs<ThemeInfo>(configJson);

            //load if there are any templates
            var templatesDirectory = _localFileProvider.CombinePaths(themePath, "Views", "Templates");
            if (_localFileProvider.DirectoryExists(templatesDirectory))
            {
                //get all the files
                var templateFiles = _localFileProvider.GetFiles(templatesDirectory, "*.html");
                foreach (var templateFile in templateFiles)
                {
                    var fileName = _localFileProvider.GetFileNameWithoutExtension(templateFile);
                    _cachedThemeInfo.Templates.Add(fileName, $"Templates/{fileName}");
                }
            }
            return _cachedThemeInfo;
        }

        public string GetThemePath(string themeName)
        {
            return ServerHelper.MapPath($"~/Content/Themes/{themeName}");
        }
    }
}