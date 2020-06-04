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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvenCart.Core;
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Enum;
using EvenCart.Services.Plugins;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Theming;
using EvenCart.Services.Logger;

namespace EvenCart.Infrastructure.Plugins
{
    public class PluginAccountant : IPluginAccountant
    {
        private readonly PluginSettings _pluginSettings;
        private readonly IPluginInstallerService _pluginInstallerService;
        private readonly ILocalFileProvider _localFileProvider;
        public readonly ILogger _logger;
        private readonly IDataSerializer _dataSerializer;
        public PluginAccountant(PluginSettings pluginSettings, IPluginInstallerService pluginInstallerService, ILocalFileProvider localFileProvider, ILogger logger, IDataSerializer dataSerializer)
        {
            _pluginSettings = pluginSettings;
            _pluginInstallerService = pluginInstallerService;
            _localFileProvider = localFileProvider;
            _logger = logger;
            _dataSerializer = dataSerializer;
        }

        public IList<PluginInfo> GetInstalledPlugins()
        {
            var availablePlugins = GetAvailablePlugins();
            var installedPlugins = _pluginSettings.GetSitePlugins()
                .Where(x => x.Installed)
                .Select(x => x.PluginSystemName)
                .ToList();

            return availablePlugins.Where(x => installedPlugins.Contains(x.SystemName)).ToList();
        }

        public IList<PluginInfo> GetActivePlugins(Type type = null)
        {
            var activeStoreId = ApplicationEngine.CurrentStore.Id;
            var activePlugins = GetInstalledPlugins().Where(x => x.ActiveStoreIds.Contains(activeStoreId));
            return type != null ? activePlugins.Where(x => type.IsAssignableFrom(x.PluginType)).ToList() : activePlugins.ToList();
        }

        public IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false)
        {
            var availablePlugins = PluginLoader.GetAvailablePlugins(withWidgets);
            var sitePlugins = _pluginSettings.GetSitePlugins();
            foreach (var ap in availablePlugins)
            {
                var sp = sitePlugins.FirstOrDefault(x => x.PluginSystemName == ap.SystemName);
                if (sp != null)
                {
                    ap.Installed = sp.Installed;
                    ap.ActiveStoreIds = sp.ActiveStoreIds ?? new List<int>();
                }
            }
            return availablePlugins;
        }

        public void InstallPlugin(PluginInfo pluginInfo)
        {
            _pluginInstallerService.Install(pluginInfo);
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, true, false);
        }

        public void UninstallPlugin(PluginInfo pluginInfo)
        {
            _pluginInstallerService.Uninstall(pluginInfo);
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, false, false);
        }

        public void ActivatePlugin(PluginInfo pluginInfo)
        {
            UpdatePluginActiveStatus(pluginInfo, true);
        }

        public void DeactivatePlugin(PluginInfo pluginInfo)
        {
            UpdatePluginActiveStatus(pluginInfo, false);
        }

        private void UpdatePluginActiveStatus(PluginInfo pluginInfo, bool active)
        {
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, true, active);
        }

        public IList<WidgetInfo> GetAvailableWidgets()
        {
            var storeId = ApplicationEngine.CurrentStore.Id;
            var plugins = GetAvailablePlugins(true).Where(x => x.ActiveStoreIds == null || x.ActiveStoreIds.Contains(storeId)).ToList();
            var widgetInfos = plugins.Where(x => x.Installed && x.ActiveStoreIds.Contains(storeId)).SelectMany(x =>
            {
                return x.Widgets.Select(y => new WidgetInfo()
                {
                    PluginName = x.Name,
                    PluginSystemName = x.SystemName,
                    WidgetSystemName = y.SystemName,
                    WidgetDisplayName = y.DisplayName,
                    WidgetZones = y.WidgetZones,
                    ConfigurationUrl = y.ConfigurationUrl,
                    HasConfiguration = y.HasConfiguration,
                    SkipDragging = y.SkipDragging,
                    WidgetInstance = y
                });

            }).ToList();
            //todo:move this to separate file
            //get the widgets already part of solution
            var solutionWidgetTypes = TypeFinder.ClassesOfType<IWidget>(restrictToSolutionAssemblies: true);
            var solutionWidgets = solutionWidgetTypes.Select(x => (IWidget)DependencyResolver.Resolve(x)).ToList();
            foreach (var sw in solutionWidgets)
            {
                if (widgetInfos.Any(x => x.WidgetSystemName == sw.SystemName))
                    continue;
                widgetInfos.Add(new WidgetInfo()
                {
                    PluginName = ApplicationConfig.AppName,
                    PluginSystemName = ApplicationConfig.InbuiltWidgetPluginName,
                    WidgetSystemName = sw.SystemName,
                    WidgetDisplayName = sw.DisplayName,
                    WidgetZones = sw.WidgetZones,
                    ConfigurationUrl = sw.ConfigurationUrl,
                    HasConfiguration = sw.HasConfiguration,
                    SkipDragging = sw.SkipDragging,
                    WidgetInstance = sw
                });
            }
            return widgetInfos;
        }

        public string AddWidget(string widgetName, string pluginSystemName, string zoneName)
        {
            return _pluginSettings.AddWidget(widgetName, pluginSystemName, zoneName);
        }

        public void DeleteWidget(string id)
        {
            _pluginSettings.DeleteWidget(id);
        }

        public int GetActiveWidgetCount(string widgetZone)
        {
            var activeWidgetStatuses = _pluginSettings.GetSiteWidgets(true);
            return activeWidgetStatuses.Count(x => x.ZoneName == widgetZone);
        }

        public bool HandleZipUpload(byte[] fileBytes)
        {
            //get a temporary file from provided bytes
            var zipFile = _localFileProvider.GetTemporaryFile(fileBytes);

            //get temporary directory to extract the package
            var tempDirectory = _localFileProvider.GetTemporaryDirectory();
            try
            {
                //first extract the zip
                _localFileProvider.ExtractArchive(zipFile, tempDirectory);

                var metaJsonFile = _localFileProvider.CombinePaths(tempDirectory, "meta.json");
                if (!_localFileProvider.FileExists(metaJsonFile))
                {
                    _logger.Log<PluginAccountant>(LogLevel.Error, "Unsupported or damaged package uploaded");
                    return false;
                }

                var packageMeta =
                    _dataSerializer.DeserializeAs<PackageMeta>(_localFileProvider.ReadAllText(metaJsonFile));
                if (packageMeta?.Items == null || !packageMeta.Items.Any())
                {
                    _logger.Log<PluginAccountant>(LogLevel.Error, "Unsupported or damaged package uploaded");
                    return false;
                }

                var pluginsDirectory = ServerHelper.MapPath("~/Plugins");
                var themesDirectory = ServerHelper.MapPath("~/Content/Themes");
                //extract each supported folder to themes and plugins
                foreach (var itemMeta in packageMeta.Items)
                {
                    //skip the version not supported
                    if (!AppVersionEvaluator.IsVersionSupported(itemMeta.SupportedVersion))
                    {
                        continue;
                    }

                    var itemDirectory = _localFileProvider.CombinePaths(tempDirectory, itemMeta.Path);
                    var destinationDirectory = "";
                    destinationDirectory = _localFileProvider.CombinePaths(
                        packageMeta.Package == PackageMeta.PackageType.Plugin ? pluginsDirectory : themesDirectory,
                        packageMeta.PackageDirectoryName);

                    _localFileProvider.CopyDirectory(itemDirectory, destinationDirectory, true);

                    //load the package
                    var configFile = _localFileProvider.CombinePaths(destinationDirectory, "config.json");
                    if (packageMeta.Package == PackageMeta.PackageType.Plugin)
                    {
                        PluginLoader.LoadPluginFromConfig(configFile, true);
                    }
                    if (packageMeta.Package == PackageMeta.PackageType.Theme)
                    {
                        DependencyResolver.Resolve<IThemeProvider>().LoadTheme(destinationDirectory, true);
                    }
                }

                return true;

            }
            catch (InvalidDataException ex)
            {
                _logger.Log<PluginAccountant>(LogLevel.Error, "The package doesn't appear to be a valid zip file.", ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Log<PluginAccountant>(LogLevel.Error, ex.Message, ex);
                return false;
            }
            finally
            {
                _localFileProvider.DeleteDirectory(tempDirectory, true);
                _localFileProvider.DeleteFile(zipFile);
            }
        }

        #region Plugin Meta
        private class PackageMeta
        {
            public string PackageDirectoryName { get; set; }

            public PackageType Package { get; set; }

            public IList<PackageItem> Items { get; set; }

            public class PackageItem
            {
               public string SupportedVersion { get; set; }

               public string Path { get; set; }
            }

            public enum PackageType
            {
                Plugin,
                Theme
            }
        }
        #endregion
    }
}