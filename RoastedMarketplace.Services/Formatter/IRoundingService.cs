using RoastedMarketplace.Data.Entity.Cultures;

namespace RoastedMarketplace.Services.Formatter
{
    public interface IRoundingService
    {
        decimal Round(decimal input, int decimalPlaces = 2, Rounding roundingType = Rounding.Default);
    }
}