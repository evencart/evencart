using DotLiquid;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Infrastructure.Localization;

namespace RoastedMarketplace.Infrastructure.ViewEngines.Filters
{
    public static class TextFilters
    {
        public static string T(string input)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            return localizer.Localize(input);
        }

        public static string Pluralize(Context context, int input, string singular, string plural)
        {
            return string.Concat(string.Format("{0} ", input), T(input == 1 ? singular : plural));
        }

        public static string WithCurrency(Context context, decimal input)
        {
            return input.ToCurrency();
        }
    }
}