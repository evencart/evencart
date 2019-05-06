using System.Collections.Generic;

namespace EvenCart.Core.Plugins
{
    public class WidgetInfo
    {
        public string PluginName { get; set; }

        public string PluginSystemName { get; set; }

        public string WidgetSystemName { get; set; }

        public string WidgetDisplayName { get; set; }

        public IList<string> WidgetZones { get; set; }

        public string DisplayOrder { get; set; }

        public string Id { get; set; }

        public bool HasConfiguration { get; set; }

        public string ConfigurationUrl { get; set; }

        public IWidget WidgetInstance { get; set; }
    }
}