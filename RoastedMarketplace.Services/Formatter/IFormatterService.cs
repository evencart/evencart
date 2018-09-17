using F1Suite.Data.Entity.Currency;

namespace RoastedMarketplace.Services.Formatter
{
    public interface IFormatterService
    {
        string FormatCurrency(decimal amount, Currency targetCurrency, bool includeSymbol = true);
    }
}