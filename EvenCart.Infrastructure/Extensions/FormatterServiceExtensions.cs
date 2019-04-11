using EvenCart.Core.Infrastructure;
using EvenCart.Services.Formatter;

namespace EvenCart.Infrastructure.Extensions
{
    public static class FormatterServiceExtensions
    {
        public static string FormatCurrency(this IFormatterService formatterService, decimal amount, bool includeSymbol = true)
        {
            if (ApplicationEngine.IsAdmin())
                return formatterService.FormatCurrency(amount, ApplicationEngine.BaseCurrency.CultureCode,
                    includeSymbol);
            return formatterService.FormatCurrency(amount, ApplicationEngine.CurrentCurrency.CultureCode, includeSymbol);
        }

        public static string ToCurrency(this decimal amount)
        {
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            return formatterService.FormatCurrency(amount);
        }

        public static string ToCurrency(this decimal? amount)
        {
            return ToCurrency(amount ?? 0);
        }
    }
}