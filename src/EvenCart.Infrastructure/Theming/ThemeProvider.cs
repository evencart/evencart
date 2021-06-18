#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.IO;
using Genesis;
using Genesis.Data;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Providers;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeProvider : IThemeProvider
    {
        private readonly string _themeDirectory;
        private readonly ILocalFileProvider _localFileProvider;
        private readonly IDataSerializer _dataSerializer;
        
        public ThemeProvider(ILocalFileProvider localFileProvider, IDataSerializer dataSerializer)
        {
            _localFileProvider = localFileProvider;
            _dataSerializer = dataSerializer;
            _themeDirectory = ServerHelper.MapPath($"{ApplicationConfig.ThemeDirectory}");
        }

        private ThemeInfo _cachedThemeInfo = null;
        public ThemeInfo GetActiveTheme()
        {
            var defaultThemeDirectoryName = "Default";
            if (!DatabaseManager.IsDatabaseInstalled())
            {
                return GetThemeInfo(new DirectoryInfo(defaultThemeDirectoryName));
            }
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            if (_cachedThemeInfo != null && _cachedThemeInfo.DirectoryName == generalSettings.ActiveTheme)
                return _cachedThemeInfo;
            var themeDirectoryName = generalSettings.ActiveTheme;
            if(themeDirectoryName.IsNullEmptyOrWhiteSpace())
                themeDirectoryName = defaultThemeDirectoryName;
            //does theme directory exist
            var themePath = GetThemePath(themeDirectoryName);
            if (!_localFileProvider.DirectoryExists(themePath))
            {
                //reset theme path to default
                themePath = GetThemePath(defaultThemeDirectoryName);
            }
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
                    _cachedThemeInfo.Templates.TryAdd(fileName, $"Templates/{fileName}");
                }
            }
            return _cachedThemeInfo;
        }

        public string GetThemePath(string themeName)
        {
            return ServerHelper.MapPath($"{ApplicationConfig.ThemeDirectory}/{themeName}");
        }

        private IList<ThemeInfo> _themeInfos = null;
        public IList<ThemeInfo> GetAvailableThemes()
        {
            if (_themeInfos != null)
                return _themeInfos;
            _themeInfos = new List<ThemeInfo>();
            var themeDirectories = _localFileProvider.GetDirectories(_themeDirectory);
            //only the directories which have theme config files should be selected
            foreach (var dir in themeDirectories)
            {
               LoadTheme(dir);
            }

            return _themeInfos;
        }

        public ThemeInfo LoadTheme(string directoryPath, bool pendingRestart = false)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            var themeInfo = GetThemeInfo(directoryInfo);
            if (themeInfo == null)
                return null;
            _themeInfos.Add(themeInfo);
            themeInfo.PendingRestart = pendingRestart;
            return themeInfo;
        }


        public void ResetActiveTheme()
        {
            _cachedThemeInfo = null;
            _themeInfos = null;
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