using DotLiquid;

namespace RoastedMarketplace.Infrastructure.Extensions
{
    public static class ViewExtensions
    {
        public static bool IsLayoutTemplate(this Template template)
        {
            return false;
        }

        public static bool HasLayout(this Template template)
        {
            return false;
        }
    }
}