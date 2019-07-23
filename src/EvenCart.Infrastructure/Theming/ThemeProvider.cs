using System.Collections.Generic;
using System.IO;
using EvenCart.Core;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeProvider : IThemeProvider
    {
        private readonly string _themeDirectory;
        private readonly ILocalFileProvider _localFileProvider;
        private readonly IDataSerializer _dataSerializer;
        private readonly GeneralSettings _generalSettings;
        public ThemeProvider(ILocalFileProvider localFileProvider, IDataSerializer dataSerializer, GeneralSettings generalSettings)
        {
            _localFileProvider = localFileProvider;
            _dataSerializer = dataSerializer;
            _generalSettings = generalSettings;
            _themeDirectory = ServerHelper.MapPath($"{ApplicationConfig.ThemeDirectory}");
        }

        private ThemeInfo _cachedThemeInfo = null;
        public ThemeInfo GetActiveTheme()
        {
            if (_cachedThemeInfo != null && _cachedThemeInfo.DirectoryName == _generalSettings.ActiveTheme)
                return _cachedThemeInfo;
            var themeDirectoryName = _generalSettings.ActiveTheme;
            if(themeDirectoryName.IsNullEmptyOrWhiteSpace())
                themeDirectoryName = "Default";
            var themePath = GetThemePath(themeDirectoryName);
            _cachedThemeInfo = GetThemeInfo(new DirectoryInfo(themePath));

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
            return ServerHelper.MapPath($"{ApplicationConfig.ThemeDirectory}/{themeName}");
        }

        public IList<ThemeInfo> GetAvailableThemes()
        {
          
            var themeDirectories = _localFileProvider.GetDirectories(_themeDirectory);
            var themeInfos = new List<ThemeInfo>();
            //only the directories which have theme config files should be selected
            foreach (var dir in themeDirectories)
            {
                var directoryInfo = new DirectoryInfo(dir);
                var themeInfo = GetThemeInfo(directoryInfo);
                if (themeInfo == null)
                    continue;
                themeInfos.Add(themeInfo);
            }

            return themeInfos;
        }

        private ThemeInfo GetThemeInfo(DirectoryInfo directoryInfo)
        {
            var themeConfigPath = _localFileProvider.CombinePaths(_themeDirectory, directoryInfo.Name, "config.json");
            if (_localFileProvider.FileExists(themeConfigPath))
            {
                var configJson = _localFileProvider.ReadAllText(themeConfigPath);
                var themeInfo = _dataSerializer.DeserializeAs<ThemeInfo>(configJson);
                var imagePath = _localFileProvider.CombinePaths(_themeDirectory, directoryInfo.Name, "Assets", "theme.jpg");
                //do we have an image file?
                if (_localFileProvider.FileExists(imagePath))
                {
                    themeInfo.ThumbnailUrl = $"/{directoryInfo.Name}/assets/theme.jpg";
                }

                themeInfo.DirectoryName = directoryInfo.Name;
                return themeInfo;
            }

            return null;
        }
    }
}