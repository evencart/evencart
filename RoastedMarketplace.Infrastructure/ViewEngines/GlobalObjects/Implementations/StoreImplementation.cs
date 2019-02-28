using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class StoreImplementation : FoundationModel
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public ThemeImplementation Theme { get; set; }

        public string LogoUrl { get; set; }

        public string CurrentPage { get; set; }
    }
}