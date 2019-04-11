namespace EvenCart.Services.Formatter
{
    public interface IFormatterService
    {
        string FormatCurrency(decimal amount, string languageCultureCode, bool includeSymbol = true);

        string FormatProductAttributes(string attributeJson);
    }
}