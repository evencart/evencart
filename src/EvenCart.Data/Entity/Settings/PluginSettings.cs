using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class PluginSettings : ISettingGroup, IGlobalSettingGroup
    {
        public string SitePlugins { get; set; }

        public string SiteWidgets { get; set; }
    }
}