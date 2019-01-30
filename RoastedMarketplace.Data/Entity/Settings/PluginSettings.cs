using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class PluginSettings : ISettingGroup
    {
        public string SitePlugins { get; set; }

        public string SiteWidgets { get; set; }
    }
}