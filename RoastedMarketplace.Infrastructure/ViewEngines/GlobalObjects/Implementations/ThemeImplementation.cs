using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class ThemeImplementation : FoundationModel
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }
}