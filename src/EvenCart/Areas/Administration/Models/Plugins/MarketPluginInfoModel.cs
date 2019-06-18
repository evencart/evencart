using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Plugins
{
    public class MarketPluginInfoModel : FoundationModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string SystemName { get; set; }

        public string Author { get; set; }

        public string AuthorUrl { get; set; }

        public string PluginUrl { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPluginUrlExternal { get; set; }

        public string Tags { get; set; }

    }
}