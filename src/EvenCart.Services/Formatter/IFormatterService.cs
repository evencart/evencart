using System;

namespace EvenCart.Services.Formatter
{
    public interface IFormatterService
    {
        string FormatCurrency(decimal amount, string languageCultureCode, bool includeSymbol = true, string customFormat = null);

        string FormatCurrencyFromIsoCode(decimal amount, string isoCode, bool includeSymbol = true, string customFormat = null);

        string FormatDateTime(DateTime dateTime, string languageCultureCode, bool onlyDate = false);

        string FormatProductAttributes(string attributeJson);
    }
}