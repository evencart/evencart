using System;
using System.Globalization;
using System.Linq;
using F1Suite.Data.Entity.Currency;

namespace RoastedMarketplace.Services.Formatter
{
    public class FormatterService : IFormatterService
    {
        public string FormatCurrency(decimal amount, Currency targetCurrency, bool includeSymbol = true)
        {
            //find out the culture for the target currency
            var culture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(c =>
            {
                var r = new RegionInfo(c.LCID);
                return string.Equals(r.ISOCurrencySymbol, targetCurrency.CurrencyCode, StringComparison.CurrentCultureIgnoreCase);
            });

            //the format of display
            var format = targetCurrency.DisplayFormat;
            var locale = targetCurrency.DisplayLocale;

            if (culture == null)
            {
                if (!string.IsNullOrEmpty(locale))
                {
                    culture = new CultureInfo(locale);
                }
            }

            if (string.IsNullOrEmpty(format))
            {
                format = culture == null || !includeSymbol ? "{0:N}" : "{0:C}";
            }

            return culture == null ? string.Format(format, amount): string.Format(culture, format, amount);
        }
    }
}