using DotLiquid;

namespace RoastedMarketplace.Infrastructure.ViewEngines
{
    public class CachedView
    {
        public Template Template { get; set; }

        public string Raw { get; set; }
    }
}
