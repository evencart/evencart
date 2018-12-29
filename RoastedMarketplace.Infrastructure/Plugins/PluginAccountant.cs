using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Services.Plugins;

namespace RoastedMarketplace.Infrastructure.Plugins
{
    public class PluginAccountant : IPluginAccountant
    {
        private readonly IPluginLoader _pluginLoader;
        private readonly PluginSettings _pluginSettings;
        private readonly IPluginInstallerService _pluginInstallerService;
        public PluginAccountant(IPluginLoader pluginLoader, PluginSettings pluginSettings, IPluginInstallerService pluginInstallerService)
        {
            _pluginLoader = pluginLoader;
            _pluginSettings = pluginSettings;
            _pluginInstallerService = pluginInstallerService;
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
            var activePlugins = GetInstalledPlugins().Where(x => x.Active);
            return type != null ? activePlugins.Where(x => type.IsAssignableFrom(x.PluginType)).ToList() : activePlugins.ToList();
        }

        public IList<PluginInfo> GetAvailablePlugins()
        {
            var availablePlugins = _pluginLoader.GetAvailablePlugins();
            var sitePlugins = _pluginSettings.GetSitePlugins();
            foreach (var ap in availablePlugins)
            {
                var sp = sitePlugins.FirstOrDefault(x => x.PluginSystemName == ap.SystemName);
                if (sp != null)
                {
                    ap.Installed = sp.Installed;
                    ap.Active = sp.Active;
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
    }
}