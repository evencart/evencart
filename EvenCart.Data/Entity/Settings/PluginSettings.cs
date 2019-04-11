using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class PluginSettings : ISettingGroup
    {
        public string SitePlugins { get; set; }

        public string SiteWidgets { get; set; }
    }
}