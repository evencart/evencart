using DotLiquid;

namespace EvenCart.Infrastructure.ViewEngines
{
    public class CachedView
    {
        public Template Template { get; set; }

        public string Raw { get; set; }
    }
}
